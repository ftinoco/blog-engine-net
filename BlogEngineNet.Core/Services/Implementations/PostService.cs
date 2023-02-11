using BlogEngineNet.Core.Domain;
using BlogEngineNet.Core.Domain.Entities;
using BlogEngineNet.Core.Domain.Persistence;
using BlogEngineNet.Core.Models;
using BlogEngineNet.Core.Services.Interfaces;

namespace BlogEngineNet.Core.Services.Implementations
{
    public class PostService : IPostService
    {
        public readonly IApplicationDbContext _context;
        public PostService(IApplicationDbContext context)
        {
            _context = context;
        }

        public Result<IEnumerable<PostModel>> GetAllPublishedPost()
        {
            var result = new Result<IEnumerable<PostModel>>();
            try
            {
                IQueryable<Post> posts = _context.Posts.Where(p =>
                                        p.Status == PostStatus.Published
                                    );
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

        #region Private methods

        private static IEnumerable<PostModel> MapPostModel(IQueryable<Post> posts)
        {
            foreach (var item in posts)
                yield return new PostModel()
                {
                    Author = string.Concat(item.Author.FirstName, " ", item.Author.LastName),
                    Content = item.Content,
                    PublicationDate = item.Trackings.Where(x =>
                                            x.LastStatus &&
                                            x.PostStatus == PostStatus.Published
                                      ).FirstOrDefault().LastModifiedDate,
                    Title = item.Title
                };
        }

        #endregion

    }
}
