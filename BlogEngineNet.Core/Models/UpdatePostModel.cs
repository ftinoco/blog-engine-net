using System.ComponentModel.DataAnnotations;

namespace BlogEngineNet.Core.Models
{
    public class UpdatePostModel
    {
        [Required]
        public Guid PostId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        [Required]
        public int AuthorId { get; set; }
    }
}
