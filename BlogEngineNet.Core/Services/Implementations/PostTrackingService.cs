using BlogEngineNet.Core.Domain.Entities;
using BlogEngineNet.Core.Domain;
using BlogEngineNet.Core.Domain.Persistence;
using BlogEngineNet.Core.Models;
using BlogEngineNet.Core.Services.Interfaces;
using BlogEngineNet.Core.Models.Blog;

namespace BlogEngineNet.Core.Services.Implementations;

public class PostTrackingService : IPostTrackingService
{
    private readonly IApplicationDbContext _context;

    public PostTrackingService(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public Result<bool> SaveTracking(CreateTrackingModel model)
    {
        Result<bool> result = new();
        try
        { 
            var lastTracking = _context.PostTrackings.Where(t => t.PostId == model.PostId)
                                       .OrderByDescending(x => x.LastModifiedDate).FirstOrDefault();  
            if (lastTracking != null)
            {
                lastTracking.LastStatus = false;
                _context.PostTrackings.Update(lastTracking);
            }

            _context.PostTrackings.Add(new PostTracking()
            {
                Comments = model.Comments,
                LastModifiedBy = model.LastModifiedBy,
                LastModifiedDate = DateTime.Now,
                LastStatus = true,
                PostId = model.PostId,
                PostStatus = model.PostStatus,
                ReviewerId = model.ReviewerId,
            });
            _context.SaveChanges();

            result.Value = true;
            result.ResultType = ResultType.SUCCESS;
            result.Message = "The update was registred";
        }
        catch (Exception ex)
        {
            result.Exception = ex;
        }
        return result;
    }
}