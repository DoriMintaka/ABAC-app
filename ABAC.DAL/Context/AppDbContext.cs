using ABAC.DAL.Configuration;
using ABAC.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ABAC.DAL.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<Rule> Rules { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ResourceConfiguration());
            modelBuilder.ApplyConfiguration(new RuleConfiguration());
        }
    }
}
