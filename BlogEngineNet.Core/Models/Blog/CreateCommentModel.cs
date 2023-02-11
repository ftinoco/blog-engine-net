using System.ComponentModel.DataAnnotations;

namespace BlogEngineNet.Core.Models.Blog;

public class CreateCommentModel
{
    [Required]
    [MaxLength(512)]
    public string Content { get; set; }
    [Required]
    public int UserId { get; set; }
    [Required]
    public Guid PostId { get; set; }
}