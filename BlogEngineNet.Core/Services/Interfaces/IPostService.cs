using BlogEngineNet.Core.Models;

namespace BlogEngineNet.Core.Services.Interfaces;

public interface IPostService
{
    Result<IEnumerable<PostModel>> GetAllPublishedPost();
}