namespace BlogEngineNet.Core.Models.Blog;

public class PostModel
{
    public Guid PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime? PublicationDate { get; set; }
    public string Author { get; set; }
    public string Status { get; set; }

    public List<CommentModel> UserComments { get; set; }
    public List<CommentModel> EditorComments { get; set; }
}