using Microsoft.EntityFrameworkCore;
using TaskManagement.DAL.Entities;
using TaskManagement.DAL.Interfaces;

namespace TaskManagement.DAL.Repositories
{
    public class UserRepository : GenericRepository<Entities.User>, IUserRepository
    {
        public UserRepository(Data.AppDbContext context) : base(context) { }

        public async Task<bool> ExistByEmailAsync(string email)
        {
            var user = await _dbSet.FirstOrDefaultAsync(u => u.Email == email);

            if (user != null)
            {
                return true;
            }

            return false;
        }

        public async Task<User?> GetUserByNameAsync(string name)
        {
            var user = await _dbSet.FirstOrDefaultAsync(u => u.Name == name);
            return user;
        }
    }
}
