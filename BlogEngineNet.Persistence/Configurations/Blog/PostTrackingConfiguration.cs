using BlogEngineNet.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogEngineNet.Persistence.Configurations.Blog;

internal class PostTrackingConfiguration
{
	public PostTrackingConfiguration(EntityTypeBuilder<PostTracking> entityBuilder)
	{
        entityBuilder.HasKey(x => x.PostTrackingId);
        entityBuilder.HasIndex(x => x.PostTrackingId).IsUnique();
        entityBuilder.Property(x => x.PostTrackingId)
                     .HasColumnName("Id")
                     .HasDefaultValueSql("NEWID()");
        entityBuilder.Property(x => x.Comments).HasMaxLength(512);
        entityBuilder.Property(x => x.ReviewerId);
        entityBuilder.Property(x => x.PostStatus).HasConversion<int>().IsRequired();
        entityBuilder.Property(x => x.LastStatus).HasColumnType("bit").IsRequired();
        entityBuilder.Property(x => x.LastModifiedBy).HasMaxLength(64);
        entityBuilder.Property(x => x.LastModifiedDate).HasDefaultValueSql("GETDATE()");

        entityBuilder.HasOne(x => x.Post)
                     .WithMany(y => y.Trackings)
                     .HasForeignKey(z => z.PostId)
                     .HasConstraintName("FK_Post_Tracking");
         
        entityBuilder.ToTable("PostTracking", schema: "Blog");
    }
}