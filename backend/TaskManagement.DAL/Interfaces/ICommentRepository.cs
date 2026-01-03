namespace TaskManagement.DAL.Interfaces
{
    public interface ICommentRepository : IGenericRepository<Entities.Comment>
    {
        Task<IEnumerable<Entities.Comment>> GetCommentsByTaskIdAsync(Guid taskId);
        Task<IEnumerable<Entities.Comment>> GetCommentsByUserIdAsync(Guid userId);
        Task<int> CountCommentsByTaskIdAsync(Guid taskId);
    }
}
