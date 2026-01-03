using Microsoft.EntityFrameworkCore;
using TaskManagement.DAL.Entities;
using Task = TaskManagement.DAL.Entities.Task;
using TaskStatus = TaskManagement.DAL.Entities.TaskStatus;

namespace TaskManagement.DAL.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Project> Projects => Set<Project>();
        public DbSet<Task> Tasks => Set<Task>();
        public DbSet<TaskStatus> TaskStatuses => Set<TaskStatus>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Comment> Comments => Set<Comment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1. User -> Project (One-to-Many: One Owner has Many Projects)
            modelBuilder.Entity<Project>()
                .HasOne(p => p.Owner)
                .WithMany(u => u.Projects)
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            // 2. Project -> Task (One-to-Many: One Project has Many Tasks)
            modelBuilder.Entity<Task>()
                .HasOne(t => t.Project)
                .WithMany(p => p.Tasks)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            // 3. Task -> TaskStatus (Many-to-One: Many Tasks have One Status)
            modelBuilder.Entity<Task>()
                .HasOne(t => t.Status)
                .WithMany()
                .HasForeignKey(t => t.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            // 4. Task -> Comment (One-to-Many: One Task has Many Comments)
            modelBuilder.Entity<Comment>()
                .HasOne(p => p.Task)
                .WithMany(t => t.Comments)
                .HasForeignKey(p => p.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

            // 5. User -> Comment (One-to-Many: One User has Many Comments)
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
