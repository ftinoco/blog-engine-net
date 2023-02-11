using BlogEngineNet.Core.Domain;
using System.ComponentModel.DataAnnotations;

namespace BlogEngineNet.Core.Models.Blog;

public class CreateTrackingModel
{
    [Required]
    public Guid PostId { get; set; }
    public int? ReviewerId { get; set; }
    [Required]
    public PostStatus PostStatus { get; set; }
    [MaxLength(512)]
    public string Comments { get; set; }
    [Required]
    public string LastModifiedBy { get; set; }
}