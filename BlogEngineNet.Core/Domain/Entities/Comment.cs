namespace BlogEngineNet.Core.Domain.Entities;

public class Comment: AuditableEntity
{
    public Guid CommentId { get; set; }
    public string Content { get; set; }
    public Guid PostId { get; set; }

    public Post Post { get; set; }
}