using BlogEngineNet.Core.Models;
using BlogEngineNet.Core.Models.Blog;

namespace BlogEngineNet.Core.Services.Interfaces;

public interface ICommentService
{
    Result<bool> AddCommentToPost(CreateCommentModel comment);
}