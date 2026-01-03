using Microsoft.EntityFrameworkCore;
using TaskManagement.DAL.Entities;
using TaskManagement.DAL.Repositories;
using TaskManagement.DAL.Tests.TestHelpers;

namespace TaskManagement.DAL.Tests.RepositoryTests
{
    public class GenericRepositoryTests
    {
        [Fact]
        public async System.Threading.Tasks.Task AddAsync_Should_Add_Entity()
        {
            using var context = DbContextFactory.Create();
            var repository = new GenericRepository<Project>(context);

            var project = new Project
            {
                Id = Guid.NewGuid(),
                Title = "Generic repo test",
                Description = "Generic repo test description",
            };

            await repository.AddAsync(project);
            await context.SaveChangesAsync();

            Assert.Equal(1, await context.Projects.CountAsync());
        }

        [Fact]
        public async System.Threading.Tasks.Task GetByIdAsync_Should_Return_Entity()
        {
            using var context = DbContextFactory.Create();
            var project = new Project
            {
                Id = Guid.NewGuid(),
                Title = "Generic repo test",
                Description = "Generic repo test description",
            };

            context.Projects.Add(project);
            await context.SaveChangesAsync();

            var repository = new GenericRepository<Project>(context);

            var result = await repository.GetByIdAsync(project.Id);

            Assert.NotNull(result);
        }

        [Fact]
        public async System.Threading.Tasks.Task Update_Should_Modify_Entity()
        {
            using var context = DbContextFactory.Create();
            var project = new Project
            {
                Id = Guid.NewGuid(),
                Title = "Generic repo test - Old title",
                Description = "Generic repo test description",
            };

            context.Projects.Add(project);
            await context.SaveChangesAsync();

            var repository = new GenericRepository<Project>(context);
            project.Title = "Generic repot test - New Title";

            repository.Update(project);
            await context.SaveChangesAsync();

            var updated = await context.Projects.FirstAsync();
            Assert.Equal("Generic repot test - New Title", updated.Title);
        }

        [Fact]
        public async System.Threading.Tasks.Task Delete_Should_Remove_Entity()
        {
            using var context = DbContextFactory.Create();
            var project = new Project
            {
                Id = Guid.NewGuid(),
                Title = "Generic repo test",
                Description = "Generic repo test description",
            };

            context.Projects.Add(project);
            await context.SaveChangesAsync();

            var repository = new GenericRepository<Project>(context);
            repository.Delete(project);
            await context.SaveChangesAsync();

            Assert.Empty(await context.Projects.ToListAsync());
        }
    }
}
