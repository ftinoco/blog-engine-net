using BlogEngineNet.Core.Domain.Entities;
using BlogEngineNet.Core.Models;

namespace BlogEngineNet.Core.Services.Interfaces;

public interface IPostService
{
    Result<Post> GetById(Guid postId);
    Result<IEnumerable<PostModel>> GetAllPublishedPost();
    Result<IEnumerable<PostModel>> GetAllPendingPost();
    Result<bool> RejectPost(RejectPostModel model);
    Result<bool> ApprovePost(ApprovePostModel model);
}