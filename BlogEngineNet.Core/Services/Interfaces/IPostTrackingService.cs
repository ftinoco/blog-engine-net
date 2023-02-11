using BlogEngineNet.Core.Models;
using BlogEngineNet.Core.Models.Blog;

namespace BlogEngineNet.Core.Services.Interfaces
{
    public interface IPostTrackingService
    {
        Result<bool> SaveTracking(CreateTrackingModel model);
    }
}