using BlogEngineNet.Core.Domain;
using BlogEngineNet.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogEngineNet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }

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
    }
} 