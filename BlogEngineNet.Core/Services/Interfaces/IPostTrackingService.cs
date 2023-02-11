using BlogEngineNet.Core.Models;

namespace BlogEngineNet.Core.Services.Interfaces
{
    public interface IPostTrackingService
    {
        Result<bool> SaveTracking(CreateTrackingModel model);
    }
}