using ABAC.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ABAC.DAL.Configuration
{
    public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder.ToTable("resource").HasKey(item => item.Id);
            builder.Property(user => user.Name).HasColumnName("name");
            builder.Property(user => user.Value).HasColumnName("value");
            builder.HasMany(user => user.Attributes).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
