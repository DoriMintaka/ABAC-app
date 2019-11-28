using ABAC.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ABAC.DAL.Configuration
{
    public class RuleConfiguration : IEntityTypeConfiguration<Rule>
    {
        public void Configure(EntityTypeBuilder<Rule> builder)
        {
            builder.ToTable("resource").HasKey(item => item.Id);
            builder.Property(user => user.Value).HasColumnName("value");
        }
    }
}
