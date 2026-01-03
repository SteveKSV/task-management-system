using TaskManagement.DAL.Entities;
using TaskManagement.DAL.Repositories;
using TaskManagement.DAL.Tests.TestHelpers;

namespace TaskManagement.DAL.Tests.RepositoryTests
{
    public class ProjectRepositoryTests
    {
        [Fact]
        public async System.Threading.Tasks.Task SearchByTitleAsync_Returns_Matching_Projects()
        {
            using var context = DbContextFactory.Create();
            var repository = new ProjectRepository(context);

            var project1 = new Project
            {
                Id = Guid.NewGuid(),
                Title = "Test Project",
                Description = "Description 1",
                OwnerId = Guid.NewGuid()
            };

            var project2 = new Project
            {
                Id = Guid.NewGuid(),
                Title = "Another Test Project",
                Description = "Description 2",
                OwnerId = Guid.NewGuid()
            };

            context.Projects.AddRange(project1, project2);
            await context.SaveChangesAsync();

            var results = await repository.SearchByTitleAsync("Test");

            Assert.NotEmpty(results);
            Assert.Contains(results, p => p.Id == project1.Id);
            Assert.Contains(results, p => p.Id == project2.Id);
        }

        [Fact]
        public async System.Threading.Tasks.Task SearchByTitleAsync_Returns_Empty_When_No_Match()
        {
            using var context = DbContextFactory.Create();
            var repository = new ProjectRepository(context);

            var project = new Project
            {
                Id = Guid.NewGuid(),
                Title = "Some Project",
                Description = "Description",
                OwnerId = Guid.NewGuid()
            };

            context.Projects.Add(project);
            await context.SaveChangesAsync();

            var results = await repository.SearchByTitleAsync("NonExisting");

            Assert.Empty(results);
        }

        [Fact]
        public async System.Threading.Tasks.Task SearchByTitleAsync_Respects_Limit()
        {
            using var context = DbContextFactory.Create();
            var repository = new ProjectRepository(context);

            for (int i = 0; i < 20; i++)
            {
                context.Projects.Add(new Project
                {
                    Id = Guid.NewGuid(),
                    Title = $"Project {i}",
                    Description = "$Description {i}",
                    OwnerId = Guid.NewGuid()
                });
            }

            await context.SaveChangesAsync();

            var results = await repository.SearchByTitleAsync("Project", limit: 5);

            Assert.Equal(5, results.Count());
        }

    }
}
