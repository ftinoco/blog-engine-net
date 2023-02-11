namespace BlogEngineNet.Core.Models;

public class CommentModel
{
    public string Content { get; set; }
    public int UserId { get; set; }
    public DateTime Date { get; set; }
}