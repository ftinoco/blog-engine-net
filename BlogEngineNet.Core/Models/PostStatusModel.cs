using BlogEngineNet.Core.Domain;
using System.ComponentModel.DataAnnotations;

namespace BlogEngineNet.Core.Models;

public class PostStatusModel
{
    [Required]
    public Guid PostId { get; set; }
    [Required]
    public int UserId { get; set; } 
    public virtual PostStatus Status { get; }
}

public class ApprovePostModel: PostStatusModel
{
    public override PostStatus Status => PostStatus.Published;
}

public class RejectPostModel: PostStatusModel
{
    public override PostStatus Status => PostStatus.Rejected;
    public string Comment { get; set; }
}

public class SubmitPostModel : PostStatusModel
{
    public override PostStatus Status => PostStatus.Submitted; 
}