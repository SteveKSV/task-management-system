using TaskManagement.DAL.Entities;

namespace TaskManagement.DAL.Interfaces
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        Task<IEnumerable<Project>> GetProjectsByOwnerIdAsync(Guid ownerId);
        Task<IEnumerable<Project>> SearchByTitleAsync(string query, int limit = 10);
    }
}
