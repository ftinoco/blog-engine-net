using BlogEngineNet.Core.Domain;
using BlogEngineNet.Core.Domain.Entities;
using BlogEngineNet.Core.Domain.Persistence;
using BlogEngineNet.Core.Models;
using BlogEngineNet.Core.Models.Blog;
using BlogEngineNet.Core.Services.Interfaces;

namespace BlogEngineNet.Core.Services.Implementations;

public class CommentService : ICommentService
{
    private readonly IApplicationDbContext _context;
    private readonly IPostService _postService;
    private readonly IUserService _userService;

    public CommentService(
        IApplicationDbContext context,
        IPostService postService,
        IUserService userService)
    {
        _context = context;
        _postService = postService;
        _userService = userService;
    }

    public Result<bool> AddCommentToPost(CreateCommentModel comment)
    {
        Result<bool> result = new();
        var postResult = _postService.GetById(comment.PostId);
        if (postResult.ResultType != ResultType.SUCCESS)
        {
            result.ResultType = ResultType.ERROR;
            result.Message = postResult.Message;
            return result;
        }

        var userResult = _userService.GetById(comment.UserId);
        if (userResult.ResultType != ResultType.SUCCESS)
        {
            result.ResultType = ResultType.ERROR;
            result.Message = userResult.Message;
            return result;
        }

        try
        {
            _context.Comments.Add(new Comment()
            {
                Content = comment.Content,
                CreatedBy = userResult.Value.Username,
                CreatedDate = DateTime.Now,
                PostId = comment.PostId
            });
            _context.SaveChanges(); 

            result.Value = true;
            result.ResultType = ResultType.SUCCESS;
            result.Message = "Comment submitted";
        }
        catch (Exception ex)
        {
            result.Exception = ex;
        } 
        return result;
    }
}