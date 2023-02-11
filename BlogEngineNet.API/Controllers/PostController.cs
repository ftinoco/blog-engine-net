using BlogEngineNet.Core.Domain;
using BlogEngineNet.Core.Models;
using BlogEngineNet.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BlogEngineNet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;

        public PostController(IPostService postService, ICommentService commentService)
        {
            _postService = postService;
            _commentService = commentService;
        }

        #region Public

        [HttpGet("GetAllPublishedPosts")]
        public IActionResult Get()
        {
            var result = _postService.GetAllPublishedPost();
            return result.ResultType switch
            {
                ResultType.SUCCESS => Ok(result),
                ResultType.ERROR => BadRequest(result.Message),
                ResultType.INFO => NotFound(result.Message),
            };
        }

        [HttpPost("AddPostComment")]
        public IActionResult PostComment([FromBody] CommentModel comment)
        {
            var result = _commentService.AddCommentToPost(comment);
            return result.ResultType switch
            {
                ResultType.SUCCESS => Ok(result),
                ResultType.ERROR => BadRequest(result.Message)
            };
        }

        #endregion

        #region Editor

        [HttpGet("GetAllPendingPost")]
        [Authorize(Roles = nameof(Role.Editor))]
        public IActionResult GetAllPendingPost()
        {
            var result = _postService.GetAllPendingPost();
            return result.ResultType switch
            {
                ResultType.SUCCESS => Ok(result),
                ResultType.ERROR => BadRequest(result.Message),
                ResultType.INFO => NotFound(result.Message),
            };
        }

        [HttpPatch("ApprovePost")]
        [Authorize(Roles = nameof(Role.Editor))]
        public IActionResult ApprovePost([FromBody] ApprovePostModel model)
        {
            var result = _postService.ApprovePost(model);
            return result.ResultType switch
            {
                ResultType.SUCCESS => Ok(result),
                ResultType.ERROR => BadRequest(result.Message)
            };
        }

        [HttpPatch("RejectPost")]
        [Authorize(Roles = nameof(Role.Editor))]
        public IActionResult RejectPost([FromBody] RejectPostModel model)
        {
            var result = _postService.RejectPost(model);
            return result.ResultType switch
            {
                ResultType.SUCCESS => Ok(result),
                ResultType.ERROR => BadRequest(result.Message)
            };
        }

        #endregion

        #region Writer

        [HttpGet("GetAllPostByAuthor/{authorId}")]
        [Authorize(Roles = nameof(Role.Writer))]
        public IActionResult GetAllPostByAuthor(int authorId)
        {
            var result = _postService.GetAllPostByAuthor(authorId);
            return result.ResultType switch
            {
                ResultType.SUCCESS => Ok(result),
                ResultType.ERROR => BadRequest(result.Message),
                ResultType.INFO => NotFound(result.Message),
            };
        }

        [HttpPatch("SubmitPost")]
        [Authorize(Roles = nameof(Role.Writer))]
        public IActionResult SubmitPost([FromBody] SubmitPostModel model)
        {
            var result = _postService.SubmitPost(model);
            return result.ResultType switch
            {
                ResultType.SUCCESS => Ok(result),
                ResultType.ERROR => BadRequest(result.Message)
            };
        }

        [HttpPost("CreatePost")]
        [Authorize(Roles = nameof(Role.Writer))]
        public IActionResult CreatePost([FromBody] CreatePostModel model)
        {
            var result = _postService.CreatePost(model);
            return result.ResultType switch
            {
                ResultType.SUCCESS => Ok(result),
                ResultType.ERROR => BadRequest(result.Message)
            };
        }

        [HttpPut("UpdatePost")]
        [Authorize(Roles = nameof(Role.Writer))]
        public IActionResult UpdatePost([FromBody] UpdatePostModel model)
        {
            var result = _postService.UpdatePost(model);
            return result.ResultType switch
            {
                ResultType.SUCCESS => Ok(result),
                ResultType.ERROR => BadRequest(result.Message)
            };
        }

        #endregion
    }
}