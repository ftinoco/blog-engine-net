using BlogEngineNet.Core.Domain;
using BlogEngineNet.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogEngineNet.Persistence.Configurations.Blog;

internal class PostConfiguration
{
    public PostConfiguration(EntityTypeBuilder<Post> entityBuilder)
    {
        entityBuilder.HasKey(x => x.PostId);
        entityBuilder.HasIndex(x => x.PostId).IsUnique();
        entityBuilder.Property(x => x.PostId)
                     .HasColumnName("Id")
                     .HasDefaultValueSql("NEWID()");
        entityBuilder.Property(x => x.Title).IsRequired().HasMaxLength(128);
        entityBuilder.Property(x => x.Content).IsRequired().HasMaxLength(512);
        entityBuilder.Property(x => x.Status).HasConversion<int>().IsRequired();

        entityBuilder.Property(x => x.CreatedBy).IsRequired().HasMaxLength(64);
        entityBuilder.Property(x => x.LastModifiedBy).HasMaxLength(64);
        entityBuilder.Property(x => x.CreatedDate).HasDefaultValueSql("GETDATE()");

        entityBuilder.HasOne(x => x.Author)
                     .WithMany(y => y.Posts)
                     .HasForeignKey(z => z.AuthorId)
                     .HasConstraintName("FK_Post_Author");

        entityBuilder.ToTable("Post", schema: "Blog");
    }
}