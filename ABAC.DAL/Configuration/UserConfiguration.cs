using ABAC.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ABAC.DAL.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user").HasKey(item => item.Id);
            builder.Property(user => user.Login).HasColumnType("nvarchar(50)").HasColumnName("login");
            builder.Property(user => user.Password).HasColumnName("password");
            builder.Property(user => user.Name).HasColumnName("name");
            builder.HasMany(user => user.Attributes);
        }
    }
}
