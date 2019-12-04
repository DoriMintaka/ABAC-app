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

            modelBuilder.Entity<User>().HasData(new User { Id = 1, Login = "admin", Password = "password",Name = "admin"});
            modelBuilder.Entity<Attribute>().HasData(
                new
                    {
                        Id = 1,
                        Name = "role",
                        Value = "admin",
                        UserId = 1,
                        ResourceId = (int?)null
                    },
                new
                    {
                        Id = 2,
                        Name = "id",
                        Value = "1",
                        UserId = 1,
                        ResourceId = (int?)null
                });

            modelBuilder.Entity<Rule>().HasData(
                new Rule
                    {
                        Id = 1,
                        Value =
                            "{\"type\": \"single\",\"value\": {\"left\": \"user.id\",\"right\": \"resource.createdby\",\"operation\": \"stringequal\"}}"
                    });
        }
    }
}
