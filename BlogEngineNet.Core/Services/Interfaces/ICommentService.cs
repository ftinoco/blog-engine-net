using BlogEngineNet.Core.Models;

namespace BlogEngineNet.Core.Services.Interfaces;

public interface ICommentService
{
    bool AddCommentToPost(CommentModel comment);
}