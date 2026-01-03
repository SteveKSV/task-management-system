using TaskManagement.DAL.Data;
using TaskManagement.DAL.Interfaces;

namespace TaskManagement.DAL.Repositories
{
    public class TaskStatusRepository : GenericRepository<Entities.TaskStatus>, ITaskStatusRepository
    {
        public TaskStatusRepository(AppDbContext context) : base(context) {}
    }
}
