namespace BlogEngineNet.Core.Domain.Entities;

public class Post: AuditableEntity
{
    public Guid PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public PostStatus Status { get; set; }

    public IQueryable<Comment> Comments { get; set; }
    public IQueryable<PostTracking> Trackings { get; set; }
}