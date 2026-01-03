using Microsoft.EntityFrameworkCore;
using TaskManagement.DAL.Data;
using TaskManagement.DAL.Interfaces;

namespace TaskManagement.DAL.Repositories
{
    public class TaskRepository : GenericRepository<Entities.Task>, ITaskRepository
    {
        public TaskRepository(AppDbContext context) : base(context) {}

        public async Task<IEnumerable<Entities.Task>> GetTasksByProjectIdAsync(Guid projectId)
        {
            var tasks = await _dbSet.Where(t => t.ProjectId == projectId).ToListAsync();
            return tasks;
        }
    }
}
