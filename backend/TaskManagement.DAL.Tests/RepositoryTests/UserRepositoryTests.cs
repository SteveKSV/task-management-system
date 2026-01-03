using TaskManagement.DAL.Entities;
using TaskManagement.DAL.Repositories;
using TaskManagement.DAL.Tests.TestHelpers;

namespace TaskManagement.DAL.Tests.RepositoryTests
{
    public class UserRepositoryTests
    {
        [Fact]
        public async System.Threading.Tasks.Task GetUserByNameAsync_Returns_User()
        {
            using var context = DbContextFactory.Create();
            var repository = new UserRepository(context);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = "Test User",
                Email = "test@gmail.com",
                PasswordHash = "hashedpassword",
            };

            context.Users.Add(user);
            await context.SaveChangesAsync();

            var result = await repository.GetUserByNameAsync("Test User");

            Assert.NotNull(result);
            Assert.Equal(user.Id, result!.Id);
        }

        [Fact]
        public async System.Threading.Tasks.Task ExistsByEmailAsync_Returns_True()
        {
            using var context = DbContextFactory.Create();
            var repo = new UserRepository(context);
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = "Test User",
                Email = "test@gmail.com",
                PasswordHash = "hashedpassword",
            };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var exists = await repo.ExistByEmailAsync("test@gmail.com");

            Assert.True(exists);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetUserByNameAsync_Returns_Null()
        {
            using var context = DbContextFactory.Create();
            var repository = new UserRepository(context);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = "Test User",
                Email = "test@gmail.com",
                PasswordHash = "hashedpassword",
            };

            context.Users.Add(user);
            await context.SaveChangesAsync();

            var result = await repository.GetUserByNameAsync("User");

            Assert.Null(result);
        }

        [Fact]
        public async System.Threading.Tasks.Task ExistsByEmailAsync_Returns_False()
        {
            using var context = DbContextFactory.Create();
            var repo = new UserRepository(context);
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = "Test User",
                Email = "test@gmail.com",
                PasswordHash = "hashedpassword",
            };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var exists = await repo.ExistByEmailAsync("TEST@gmail.com");

            Assert.False(exists);
        }
    }
}
