namespace BlogEngineNet.Core.Domain.Entities
{
    public class PostTracking
    {
        public Guid PostTrackingId { get; set; }
        public Guid PostId { get; set; }
        public PostStatus PostStatus { get; set; }
        public string Comments { get; set; }
        public bool LastStatus { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public Post Post { get; set; }
    }
}