using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Domain.Entities;
using System.Threading.Tasks;

namespace ProjectManagementApp.Infrastructure.EF
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet for each entity in the domain
        public DbSet<Project> Projects { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Entity configuration for Project
            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(e => e.ProjectId); // Primary key

                // Other configurations (like relationships, if needed)
                entity.Property(e => e.ProjectName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.Description)
                      .HasMaxLength(500);
            });

            // Entity configuration for Task
            modelBuilder.Entity<Tasks>(entity =>
            {
                entity.HasKey(e => e.TaskId); // Primary key

                // Configuring relationships (e.g., ProjectId as foreign key)
                entity.HasOne(p => p.Project)
                      .WithMany(t => t.Tasks)
                      .HasForeignKey(p => p.ProjectId);

                entity.Property(e => e.TaskName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.Description)
                      .HasMaxLength(500);

                // Convert enums to strings in the database
                entity.Property(e => e.Priority)
                      .HasConversion<string>();

                entity.Property(e => e.Status)
                      .HasConversion<string>();
            });

            // Task and Comment relationship configuration
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Tasks)
                .WithMany(t => t.Comments)
                .HasForeignKey(c => c.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }

}
