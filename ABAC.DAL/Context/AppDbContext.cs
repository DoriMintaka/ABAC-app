using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ABAC.DAL.Entities;
using ABAC.DAL.Configuration;

namespace ABAC.DAL.Context
{
	public class AppDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Resource> Resources { get; set; }

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
			Database.Migrate();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new UserConfiguration());
			modelBuilder.ApplyConfiguration(new ResourceConfiguration());
		}
	}
}
