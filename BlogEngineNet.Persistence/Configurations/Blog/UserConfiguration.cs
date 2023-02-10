using BlogEngineNet.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogEngineNet.Persistence.Configurations.Blog;

internal class UserConfiguration
{
	public UserConfiguration(EntityTypeBuilder<User> entityBuilder)
	{
        entityBuilder.HasKey(x => x.UserId);
        entityBuilder.HasIndex(x => x.UserId).IsUnique();
        entityBuilder.Property(x => x.UserId)
                     .HasColumnName("Id")
                     .ValueGeneratedOnAdd();
        entityBuilder.Property(x => x.FirstName).IsRequired().HasMaxLength(16);
        entityBuilder.Property(x => x.LastName).IsRequired().HasMaxLength(16);
        entityBuilder.Property(x => x.Username).IsRequired().HasMaxLength(8);
        entityBuilder.Property(x => x.Role).HasConversion<int>().IsRequired();
        entityBuilder.Property(x => x.Password).IsRequired().HasMaxLength(16);

        entityBuilder.Property(x => x.CreatedBy).IsRequired().HasMaxLength(64);
        entityBuilder.Property(x => x.LastModifiedBy).HasMaxLength(64);
        entityBuilder.Property(x => x.CreatedDate).HasDefaultValueSql("GETDATE()");

        entityBuilder.ToTable("User", schema: "Blog");
    }
}