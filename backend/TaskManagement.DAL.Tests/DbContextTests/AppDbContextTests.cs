using Microsoft.EntityFrameworkCore;
using TaskManagement.DAL.Entities;
using TaskManagement.DAL.Tests.TestHelpers;
using Task = TaskManagement.DAL.Entities.Task;
using TaskStatus = TaskManagement.DAL.Entities.TaskStatus;

namespace TaskManagement.DAL.Tests.DbContextTests
{
    public class AppDbContextTests
    {
        [Fact]
        public async System.Threading.Tasks.Task Should_Save_And_Read_Project()
        {
            using var context = DbContextFactory.Create();

            var user = new User { Id = Guid.NewGuid(), Name = "testuser", Email = "testemail@gmail.com", PasswordHash = "12312312412312" };
            var project = new Project
            {
                Id = Guid.NewGuid(),
                Title = "Test Project",
                Description = "A project for testing",
                Owner = user
            };

            context.Users.Add(user);
            context.Projects.Add(project);
            await context.SaveChangesAsync();

            var saved = await context.Projects.Include(p => p.Owner).FirstOrDefaultAsync();

            Assert.NotNull(saved);
            Assert.NotNull(saved!.Owner);
        }

        [Fact]
        public async System.Threading.Tasks.Task Deleting_Project_Should_Delete_Tasks()
        {
            using var context = DbContextFactory.Create();

            var project = new Project
            {
                Id = Guid.NewGuid(),
                Title = "Test Project",
                Description = "A project for testing"
            };

            var task = new Task
            {
                Id = Guid.NewGuid(),
                Title = "Test Task",
                Description = "A task for testing",
                DueDate = DateTime.UtcNow.AddDays(7),
                Project = project,
                Priority = 1,
                Order = 1
            };

            context.Projects.Add(project);
            context.Tasks.Add(task);
            await context.SaveChangesAsync();

            context.Projects.Remove(project);
            await context.SaveChangesAsync();

            var tasks = await context.Tasks.ToListAsync();

            Assert.Empty(tasks);
        }

        [Fact]
        public async System.Threading.Tasks.Task Deleting_Task_Should_Delete_Comments()
        {
            using var context = DbContextFactory.Create();

            var task = new Task
            {
                Id = Guid.NewGuid(),
                Title = "Test Task",
                Description = "A task for testing",
                DueDate = DateTime.UtcNow.AddDays(7),
                Priority = 1,
                Order = 1
            };

            var comment = new Comment
            {
                Id = Guid.NewGuid(),
                Content = "This is a test comment",
                Task = task
            };

            context.Tasks.Add(task);
            context.Comments.Add(comment);
            await context.SaveChangesAsync();

            context.Tasks.Remove(task);
            await context.SaveChangesAsync();

            var comments = await context.Comments.ToListAsync();
            Assert.Empty(comments);
        }

        [Fact]
        public async System.Threading.Tasks.Task TaskStatus_Should_Not_Be_Deleted_When_Deleting_Task()
        {
            using var context = DbContextFactory.Create();
            var status = new TaskStatus
            {
                Id = Guid.NewGuid(),
                Name = "In Progress"
            };
            var task = new Task
            {
                Id = Guid.NewGuid(),
                Title = "Test Task",
                Description = "A task for testing",
                DueDate = DateTime.UtcNow.AddDays(7),
                Status = status,
                Priority = 1,
                Order = 1
            };
            context.TaskStatuses.Add(status);
            context.Tasks.Add(task);

            await context.SaveChangesAsync();

            context.Tasks.Remove(task);

            await context.SaveChangesAsync();

            var savedStatus = await context.TaskStatuses.FirstOrDefaultAsync(s => s.Id == status.Id);
            Assert.NotNull(savedStatus);
        }

        [Fact]
        public async System.Threading.Tasks.Task Deleting_User_Should_Set_Comment_UserId_To_Null()
        {
            using var context = DbContextFactory.Create();

            var user = new User { Id = Guid.NewGuid(), Name = "TestUser", PasswordHash = "testpassordhash123", Email = "testemail@gmail.com" };
            var comment = new Comment
            {
                Id = Guid.NewGuid(),
                User = user, Content = "TestContent", UserId = user.Id
            };

            context.Users.Add(user);
            context.Comments.Add(comment);
            await context.SaveChangesAsync();

            context.Users.Remove(user);
            await context.SaveChangesAsync();

            var savedComment = await context.Comments.FirstAsync();
            Assert.Null(savedComment.UserId);
        }

    }
}
