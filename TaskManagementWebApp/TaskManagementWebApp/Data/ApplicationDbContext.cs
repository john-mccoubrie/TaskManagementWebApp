using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Oracle.EntityFrameworkCore;
using TaskManagementWebApp.Models;

namespace TaskManagementWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Update with your Oracle connection string
                optionsBuilder.UseOracle("User Id=John;Password=TaskApp4;Data Source=localhost:1521/XEPDB1");
            }
        }

#nullable disable
        public DbSet<ProjectTask> ProjectTasks { get; set; }
#nullable enable
    }
}