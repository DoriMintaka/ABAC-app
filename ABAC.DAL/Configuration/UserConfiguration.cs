using ABAC.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AutoMapper;
using System.Linq;

namespace ABAC.DAL.Configuration
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.ToTable("user").HasKey(item => item.Id);
			builder.Property(user => user.Login).HasColumnName("login");
			builder.Property(user => user.Password).HasColumnName("password");
			builder.Property(user => user.Name).HasColumnName("name");
			builder.HasMany(user => user.Attributes);
		}
	}
}
