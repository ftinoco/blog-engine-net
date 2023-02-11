using BlogEngineNet.Core.Domain.Entities;
using BlogEngineNet.Core.Models;

namespace BlogEngineNet.Core.Services.Interfaces;

public interface IPostService
{
    // for public role
    Result<Post> GetById(Guid postId);
    Result<IEnumerable<PostModel>> GetAllPublishedPost();
    // for editor role
    Result<IEnumerable<PostModel>> GetAllPendingPost();
    Result<bool> RejectPost(RejectPostModel model);
    Result<bool> ApprovePost(ApprovePostModel model); 
    // for writer role
    Result<IEnumerable<PostModel>> GetAllPostByAuthor(int authorId);
    Result<bool> SubmitPost(SubmitPostModel model);
    Result<bool> CreatePost(CreatePostModel model);
    Result<bool> UpdatePost(UpdatePostModel model);
}