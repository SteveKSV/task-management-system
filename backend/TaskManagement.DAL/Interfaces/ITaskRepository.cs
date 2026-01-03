namespace TaskManagement.DAL.Interfaces
{
    public interface ITaskRepository : IGenericRepository<Entities.Task>
    {
        Task<IEnumerable<Entities.Task>> GetTasksByProjectIdAsync(Guid projectId);

    }
}
