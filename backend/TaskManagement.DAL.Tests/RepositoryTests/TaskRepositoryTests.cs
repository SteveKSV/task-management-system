using TaskManagement.DAL.Entities;
using TaskManagement.DAL.Repositories;
using TaskManagement.DAL.Tests.TestHelpers;
using Task = TaskManagement.DAL.Entities.Task;

namespace TaskManagement.DAL.Tests.RepositoryTests
{
    public class TaskRepositoryTests
    {
        [Fact]
        public async System.Threading.Tasks.Task GetTasksByProjectIdAsync_Returns_Tasks()
        {
            using var context = DbContextFactory.Create();
            
            var repository = new TaskRepository(context);

            var project = new Project
            {
                Id = Guid.NewGuid(),
                Title = "Sample Project",
                Description = "Sample Description",
                OwnerId = Guid.NewGuid()
            };

            var task1 = new Task
            {
                Id = Guid.NewGuid(),
                Title = "Task 1",
                Description = "Description 1",
                ProjectId = project.Id,
                Project = project
            };
            var task2 = new Task
            {
                Id = Guid.NewGuid(),
                Title = "Task 2",
                Description = "Description 2",
                ProjectId = project.Id,
                Project = project
            };

            context.Tasks.AddRange(task1, task2);
            await context.SaveChangesAsync();

            var results = await repository.GetTasksByProjectIdAsync(project.Id);

            Assert.NotEmpty(results);
            Assert.Contains(results, t => t.Id == task1.Id);
            Assert.Contains(results, t => t.Id == task2.Id);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetTasksByProjectIdAsync_Returns_Empty()
        {
            using var context = DbContextFactory.Create();

            var repository = new TaskRepository(context);

            var project = new Project
            {
                Id = Guid.NewGuid(),
                Title = "Sample Project",
                Description = "Sample Description",
                OwnerId = Guid.NewGuid()
            };

            var task1 = new Task
            {
                Id = Guid.NewGuid(),
                Title = "Task 1",
                Description = "Description 1",
                ProjectId = project.Id,
                Project = project
            };
            var task2 = new Task
            {
                Id = Guid.NewGuid(),
                Title = "Task 2",
                Description = "Description 2",
                ProjectId = project.Id,
                Project = project
            };

            context.Tasks.AddRange(task1, task2);
            await context.SaveChangesAsync();

            var projectId = Guid.NewGuid(); // Non-existing project Id

            var results = await repository.GetTasksByProjectIdAsync(projectId);

            Assert.Empty(results);
        }
    }
}
