using BlogEngineNet.Core.Domain.Entities;
using BlogEngineNet.Persistence.Configurations.Blog;
using Microsoft.EntityFrameworkCore;

namespace BlogEngineNet.Persistence.Configurations;

internal class ModelConfigurations
{
    public static void Config(ModelBuilder builder)
    {
        _ = new UserConfiguration(builder.Entity<User>());
        _ = new PostConfiguration(builder.Entity<Post>());
        _ = new CommentConfiguration(builder.Entity<Comment>());
        _ = new PostTrackingConfiguration(builder.Entity<PostTracking>());
    }
}