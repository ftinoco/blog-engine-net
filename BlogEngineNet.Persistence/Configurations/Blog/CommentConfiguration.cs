using BlogEngineNet.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogEngineNet.Persistence.Configurations.Blog;

internal class CommentConfiguration
{
    public CommentConfiguration(EntityTypeBuilder<Comment> entityBuilder)
    {
        entityBuilder.HasKey(x => x.CommentId);
        entityBuilder.HasIndex(x => x.CommentId).IsUnique();
        entityBuilder.Property(x => x.CommentId)
                     .HasColumnName("Id")
                     .HasDefaultValueSql("NEWID()");
        entityBuilder.Property(x => x.Content).HasMaxLength(512);

        entityBuilder.Property(x => x.CreatedBy).IsRequired().HasMaxLength(64);
        entityBuilder.Property(x => x.LastModifiedBy).HasMaxLength(64);
        entityBuilder.Property(x => x.CreatedDate).HasDefaultValueSql("GETDATE()");

        entityBuilder.HasOne(x => x.Post)
                     .WithMany(y => y.Comments)
                     .HasForeignKey(z => z.PostId)
                     .HasConstraintName("FK_Post_Comment");

        entityBuilder.ToTable("Comment", schema: "Blog");
    }
}