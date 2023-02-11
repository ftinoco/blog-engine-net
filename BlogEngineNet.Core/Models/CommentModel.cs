using System.ComponentModel.DataAnnotations;

namespace BlogEngineNet.Core.Models;

public class CommentModel
{
    [Required]
    [MaxLength(512)]
    public string Content { get; set; }
    [Required]
    public int UserId { get; set; } 
    [Required]
    public Guid PostId { get; set; }
}