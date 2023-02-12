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
        entityBuilder.HasData(new User
        {
            FirstName = "John",
            UserId= 1,
            CreatedBy = "Test",
            LastName = "Doe",
            Password= "SafePassword",
            Role = Core.Domain.Role.Editor,
            LastModifiedBy = "Test",
            Username = "JDoe",
        }, new User
        {
            FirstName = "Freddy",
            UserId = 2,
            CreatedBy = "Test",
            LastName = "Jackson",
            Password = "SafePassword",
            Role = Core.Domain.Role.Writer,
            LastModifiedBy = "Test",
            Username = "FJackson",
        }, new User
        {
            FirstName = "Richard",
            UserId = 3,
            CreatedBy = "Test",
            LastName = "Ashcroft",
            Password = "SafePassword",
            Role = Core.Domain.Role.Public,
            LastModifiedBy = "Test",
            Username = "Ashcroft",
        });
    }
}