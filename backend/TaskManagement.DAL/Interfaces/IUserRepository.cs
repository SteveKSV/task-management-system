using TaskManagement.DAL.Entities;

namespace TaskManagement.DAL.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetUserByNameAsync(string name);
        Task<bool> ExistByEmailAsync(string email);
    }
}
