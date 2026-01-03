namespace TaskManagement.DAL.Entities
{
    public class Comment : BaseEntity
    {
        public Guid TaskId { get; set; }
        public Task Task { get; set; } = null!;

        public Guid? UserId { get; set; }
        public User? User { get; set; }

        public string Content { get; set; } = null!;
    }
}