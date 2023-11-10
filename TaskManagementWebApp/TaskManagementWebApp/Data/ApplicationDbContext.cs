using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManagementWebApp.Models;

namespace TaskManagementWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        // DbSet properties for your model classes.
        // Add a DbSet for each entity you want to be able to store and query from the database.

        // DbSet for 'Task' entities. This will create a 'Tasks' table in your database.
        public DbSet<ProjectTask> ProjectTasks { get; set; }

        // Override the OnModelCreating method to add additional configurations
        // when the model for the database context is being built.
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Custom configurations for your entities go here.
            // Example: builder.Entity<Task>().Property(t => t.Title).IsRequired();
        }
    }
}