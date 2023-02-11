using BlogEngineNet.Core.Models;

namespace BlogEngineNet.Core.Services.Interfaces;

public interface ICommentService
{
    Result<bool> AddCommentToPost(CommentModel comment);
}