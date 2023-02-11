using System.ComponentModel.DataAnnotations;

namespace BlogEngineNet.Core.Models;

public class CreatePostModel
{
    [Required]
    public string Title { get; set; }
    [Required]
    public string Content { get; set; }
    [Required]
    public int AuthorId { get; set; }
}
