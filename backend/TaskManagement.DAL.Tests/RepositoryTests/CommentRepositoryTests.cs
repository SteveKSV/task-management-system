using System.Diagnostics.Contracts;
using TaskManagement.DAL.Entities;
using TaskManagement.DAL.Repositories;
using TaskManagement.DAL.Tests.TestHelpers;
using Task = TaskManagement.DAL.Entities.Task;

namespace TaskManagement.DAL.Tests.RepositoryTests
{
    public class CommentRepositoryTests
    {
        [Fact]
        public async System.Threading.Tasks.Task CountCommentsByTaskIdAsync_Returns_Correct_Count()
        {
            using var context = DbContextFactory.Create();
            var repository = new CommentRepository(context);
            var task = new Task
            {
                Id = Guid.NewGuid(),
                Title = "Sample Task",
                Description = "Sample Description",
                ProjectId = Guid.NewGuid()
            };

            var comment1 = new Comment
            {
                Id = Guid.NewGuid(),
                Content = "Comment 1",
                TaskId = task.Id,
                UserId = Guid.NewGuid()
            };

            var comment2 = new Comment
            {
                Id = Guid.NewGuid(),
                Content = "Comment 2",
                TaskId = task.Id,
                UserId = Guid.NewGuid()
            };

            context.Comments.AddRange(comment1, comment2);
            await context.SaveChangesAsync();

            var count = await repository.CountCommentsByTaskIdAsync(task.Id);
            Assert.Equal(2, count);
        }

        [Fact]
        public async System.Threading.Tasks.Task CountCommentsByTaskIdAsync_Returns_InCorrect_Count()
        {
            using var context = DbContextFactory.Create();
            var repository = new CommentRepository(context);
            var task = new Task
            {
                Id = Guid.NewGuid(),
                Title = "Sample Task",
                Description = "Sample Description",
                ProjectId = Guid.NewGuid()
            };

            var comment1 = new Comment
            {
                Id = Guid.NewGuid(),
                Content = "Comment 1",
                TaskId = task.Id,
                UserId = Guid.NewGuid()
            };

            var comment2 = new Comment
            {
                Id = Guid.NewGuid(),
                Content = "Comment 2",
                TaskId = task.Id,
                UserId = Guid.NewGuid()
            };

            context.Comments.AddRange(comment1, comment2);
            await context.SaveChangesAsync();

            var count = await repository.CountCommentsByTaskIdAsync(task.Id);
            Assert.NotEqual(3, count);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetCommentsByTaskIdAsync_Returns_Comments()
        {
            using var context = DbContextFactory.Create();
            var repository = new CommentRepository(context);
            var task = new Task
            {
                Id = Guid.NewGuid(),
                Title = "Sample Task",
                Description = "Sample Description",
                ProjectId = Guid.NewGuid()
            };

            var comment1 = new Comment
            {
                Id = Guid.NewGuid(),
                Content = "Comment 1",
                TaskId = task.Id,
                UserId = Guid.NewGuid()
            };

            var comment2 = new Comment
            {
                Id = Guid.NewGuid(),
                Content = "Comment 2",
                TaskId = task.Id,
                UserId = Guid.NewGuid()
            };

            context.Comments.AddRange(comment1, comment2);
            await context.SaveChangesAsync();

            var results = await repository.GetCommentsByTaskIdAsync(task.Id);

            Assert.NotEmpty(results);
            Assert.Contains(results, c => c.Id == comment1.Id);
            Assert.Contains(results, c => c.Id == comment2.Id);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetCommentsByTaskIdAsync_Returns_Empty()
        {
            using var context = DbContextFactory.Create();
            var repository = new CommentRepository(context);
            var taskId = Guid.NewGuid();
            var results = await repository.GetCommentsByTaskIdAsync(taskId);
            Assert.Empty(results);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetCommentsByUserIdAsync_Returns_Comments()
        {
            using var context = DbContextFactory.Create();
            var repository = new CommentRepository(context);
            var userId = Guid.NewGuid();

            var comment1 = new Comment
            {
                Id = Guid.NewGuid(),
                Content = "Comment 1",
                TaskId = Guid.NewGuid(),
                UserId = userId
            };

            var comment2 = new Comment
            {
                Id = Guid.NewGuid(),
                Content = "Comment 2",
                TaskId = Guid.NewGuid(),
                UserId = userId
            };

            context.Comments.AddRange(comment1, comment2);
            await context.SaveChangesAsync();

            var results = await repository.GetCommentsByUserIdAsync(userId);

            Assert.NotEmpty(results);
            Assert.Contains(results, c => c.Id == comment1.Id);
            Assert.Contains(results, c => c.Id == comment2.Id);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetCommentsByUserIdAsync_Returns_Empty()
        {
            using var context = DbContextFactory.Create();
            var repository = new CommentRepository(context);
            var userId = Guid.NewGuid();
            var results = await repository.GetCommentsByUserIdAsync(userId);
            Assert.Empty(results);
        }
    }
}
