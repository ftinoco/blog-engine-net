namespace BlogEngineNet.Core.Domain.Entities;

public class Post: AuditableEntity
{
    public Guid PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public PostStatus Status { get; set; }
    public int AuthorId { get; set; }


    public virtual User Author { get; set; }
    public IEnumerable<Comment> Comments { get; set; }
    public IEnumerable<PostTracking> Trackings { get; set; }

}