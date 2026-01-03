using Microsoft.EntityFrameworkCore;
using TaskManagement.DAL.Data;
using TaskManagement.DAL.Entities;
using TaskManagement.DAL.Interfaces;

namespace TaskManagement.DAL.Repositories
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        public ProjectRepository(AppDbContext context) : base(context) {}

        public async Task<IEnumerable<Project>> SearchByTitleAsync(string query, int limit = 10)
        {
            return await _dbSet
                .Where(p => p.Title.Contains(query))
                .OrderBy(p => p.Title)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<IEnumerable<Project>> GetProjectsByOwnerIdAsync(Guid ownerId)
        {
            var projects =  await _dbSet.Where(p => p.OwnerId == ownerId).ToListAsync();
            return projects;
        }
    }
}
