using Microsoft.EntityFrameworkCore;
using TaskManagement.DAL.Data;
using TaskManagement.DAL.Entities;
using TaskManagement.DAL.Interfaces;

namespace TaskManagement.DAL.Repositories
{
    public class CommentRepository : GenericRepository<Entities.Comment>, ICommentRepository
    {
        public CommentRepository(AppDbContext context) : base(context) {}

        public async Task<int> CountCommentsByTaskIdAsync(Guid taskId)
        {
            var commentsCount = await _dbSet.CountAsync(c => c.TaskId == taskId);
            return commentsCount;
        }

        public async Task<IEnumerable<Comment>> GetCommentsByTaskIdAsync(Guid taskId)
        {
            var comments = await _dbSet.Where(c => c.TaskId == taskId).ToListAsync();
            return comments;
        }

        public async Task<IEnumerable<Comment>> GetCommentsByUserIdAsync(Guid userId)
        {
            var comments = await _dbSet.Where(c => c.UserId == userId).ToListAsync();
            return comments;
        }
    }
}
