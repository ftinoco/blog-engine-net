using BlogEngineNet.Core.Domain;
using BlogEngineNet.Core.Domain.Entities;
using BlogEngineNet.Core.Domain.Persistence;
using BlogEngineNet.Core.Models;
using BlogEngineNet.Core.Models.Blog;
using BlogEngineNet.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogEngineNet.Core.Services.Implementations;

public class PostService : IPostService
{
    private readonly IUserService _userService;
    private readonly IApplicationDbContext _context;
    private readonly IPostTrackingService _postTrackingService;

    public PostService(
        IApplicationDbContext context,
        IPostTrackingService postTrackingService,
        IUserService userService)
    {
        _context = context;
        _userService = userService;
        _postTrackingService = postTrackingService;
    }

    public Result<IEnumerable<PostModel>> GetAllPublishedPost()
    {
        return GetAllPostByStatus(PostStatus.Published);
    }

    public Result<IEnumerable<PostModel>> GetAllPendingPost()
    {
        return GetAllPostByStatus(PostStatus.Submitted);
    }

    public Result<IEnumerable<PostModel>> GetAllPostByAuthor(int authorId)
    {
        var result = new Result<IEnumerable<PostModel>>();
        try
        {
            IQueryable<Post> posts = _context.Posts.Include(u => u.Author)
                                                   .Include(t => t.Trackings)
                                             .Where(p => p.AuthorId == authorId);
            if (posts.Any())
            {
                result.Value = MapPostModel(posts);
                result.Message = "Information obtained successfully";
                result.ResultType = ResultType.SUCCESS;
            }
            else
            {
                result.Value = new List<PostModel>();
                result.Message = "Post records not found";
                result.ResultType = ResultType.INFO;
            }
        }
        catch (Exception ex)
        {
            result.Exception = ex;
        }
        return result;
    }

    public Result<Post> GetById(Guid postId)
    {
        var result = new Result<Post>();
        try
        {
            var post = _context.Posts.SingleOrDefault(p => p.PostId == postId);
            result.Value = post;
            if (post is null)
            {
                result.Message = "Post record not found";
                result.ResultType = ResultType.INFO;
            }
            else
            {
                result.Value = post;
                result.Message = "Record obtained successfully";
                result.ResultType = ResultType.SUCCESS;
            }
        }
        catch (Exception ex)
        {
            result.Exception = ex;
            // the exception should be register in log
        }
        return result;
    }

    public Result<bool> RejectPost(RejectPostModel model)
    {
        return UpdatePostStatus(model);
    }

    public Result<bool> ApprovePost(ApprovePostModel model)
    {
        return UpdatePostStatus(model);
    }

    public Result<bool> SubmitPost(SubmitPostModel model)
    {
        return UpdatePostStatus(model);
    }

    public Result<bool> CreatePost(CreatePostModel model)
    {
        Result<bool> result = new();
        using var transaction = ((DbContext)_context).Database.BeginTransaction();
        try
        {
            var userResult = _userService.GetById(model.AuthorId);
            if (userResult.ResultType != ResultType.SUCCESS)
            {
                result.ResultType = ResultType.ERROR;
                result.Message = userResult.Message;
                return result;
            }
            var post = new Post()
            {
                AuthorId = model.AuthorId,
                Content = model.Content,
                CreatedBy = userResult.Value.Username,
                CreatedDate = DateTime.Now,
                Status = PostStatus.New,
                Title = model.Title,
                LastModifiedBy= userResult.Value.Username,
            };

            _context.Posts.Add(post);
            _context.SaveChanges();

            result = _postTrackingService.SaveTracking(new CreateTrackingModel()
            { 
                LastModifiedBy = userResult.Value.Username,
                PostId = post.PostId,
                PostStatus = PostStatus.New,
            });

            if (result.ResultType == ResultType.SUCCESS)
            {
                result.Value = true;
                result.ResultType = ResultType.SUCCESS;
                result.Message = "Post saved";
                transaction.Commit();
            }
            else
                throw result.Exception;

        }
        catch (Exception ex)
        {
            result.Exception = ex;
            transaction.Rollback();
        }
        return result;
    }

    public Result<bool> UpdatePost(UpdatePostModel model)
    {
        Result<bool> result = new();
        using var transaction = ((DbContext)_context).Database.BeginTransaction();
        try
        {
            var userResult = _userService.GetById(model.AuthorId);
            if (userResult.ResultType != ResultType.SUCCESS)
            {
                result.ResultType = ResultType.ERROR;
                result.Message = userResult.Message;
                return result;
            }

            var post = _context.Posts.SingleOrDefault(p => p.PostId == model.PostId);

            if (post != null)
            { 
                if(post.Status != PostStatus.New && post.Status != PostStatus.Rejected)
                {
                    result.ResultType = ResultType.ERROR;
                    result.Message = "The post should be new or rejected to update";
                    return result;
                }

                post.LastModifiedDate = DateTime.Now;
                post.LastModifiedBy = userResult.Value.Username;
                post.Title = model.Title ?? post.Title;
                post.Content= model.Content ?? post.Content;

                _context.Posts.Update(post);
                _context.SaveChanges();

                result = _postTrackingService.SaveTracking(new CreateTrackingModel()
                { 
                    LastModifiedBy = userResult.Value.Username, 
                    PostId = post.PostId,
                    PostStatus = post.Status,
                });

                if (result.ResultType == ResultType.SUCCESS)
                {
                    result.Value = true;
                    result.ResultType = ResultType.SUCCESS;
                    result.Message = "Post updated";
                    transaction.Commit();
                }
                else
                    throw result.Exception;
            }
            else
            {
                result.Message = "Post record not found";
                result.ResultType = ResultType.ERROR;
            }
        }
        catch (Exception ex)
        {
            result.Exception = ex;
            transaction.Rollback();
        }
        return result;
    }

    #region Private methods

    private static IEnumerable<PostModel> MapPostModel(IEnumerable<Post> posts)
    {
        foreach (var item in posts) {
            var trackings = item.Trackings.Where(x =>
                                        x.LastStatus &&
                                        x.PostStatus == PostStatus.Published
                                  );
            yield return new PostModel()
            {
                Author = string.Concat(item.Author.FirstName, " ", item.Author.LastName),
                Content = item.Content,
                PublicationDate = trackings.Any() ? trackings.FirstOrDefault().LastModifiedDate : null,
                Title = item.Title,
                Status = item.Status.ToString(),
            };
        }
    }

    private Result<IEnumerable<PostModel>> GetAllPostByStatus(PostStatus status)
    {
        var result = new Result<IEnumerable<PostModel>>();
        try
        {
            IQueryable<Post> posts = _context.Posts.Include(u => u.Author)
                                                   .Include(t => t.Trackings)
                                             .Where(p => p.Status == status);
            if (posts.Any())
            {
                result.Value = MapPostModel(posts);
                result.Message = "Information obtained successfully";
                result.ResultType = ResultType.SUCCESS;
            }
            else
            {
                result.Value = new List<PostModel>();
                result.Message = "Post records not found";
                result.ResultType = ResultType.INFO;
            }
        }
        catch (Exception ex)
        {
            result.Exception = ex;
            // the exception should be register in log
        }
        return result;
    }

    private Result<bool> UpdatePostStatus(PostStatusModel model)
    {
        Result<bool> result = new();
        using var transaction = ((DbContext)_context).Database.BeginTransaction();
        try
        {
            var userResult = _userService.GetById(model.UserId);
            if (userResult.ResultType != ResultType.SUCCESS)
            {
                result.ResultType = ResultType.ERROR;
                result.Message = userResult.Message;
                return result;
            }

            var post = _context.Posts.SingleOrDefault(p => p.PostId == model.PostId);

            if (post != null)
            {
                post.Status = model.Status;
                post.LastModifiedDate = DateTime.Now;
                post.LastModifiedBy = userResult.Value.Username;

                _context.Posts.Update(post);
                _context.SaveChanges();

                result = _postTrackingService.SaveTracking(new CreateTrackingModel()
                {
                    Comments = model.Status == PostStatus.Rejected ? ((RejectPostModel)model).Comment : null,
                    LastModifiedBy = userResult.Value.Username,
                    ReviewerId = model.Status is PostStatus.Rejected or PostStatus.Published ? model.UserId : null,
                    PostId = model.PostId,
                    PostStatus = model.Status,
                });

                if (result.ResultType == ResultType.SUCCESS)
                {
                    result.Value = true;
                    result.ResultType = ResultType.SUCCESS;
                    result.Message = "Post updated";
                    transaction.Commit();
                }
                else
                    throw result.Exception;
            }
            else
            {
                result.Message = "Post record not found";
                result.ResultType = ResultType.ERROR;
            }
        }
        catch (Exception ex)
        {
            result.Exception = ex;
            transaction.Rollback();
        }
        return result;
    }

    #endregion
}