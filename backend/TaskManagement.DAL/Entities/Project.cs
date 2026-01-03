namespace TaskManagement.DAL.Entities
{
    public class Project : BaseEntity
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public User Owner { get; set; } = null!;
        public Guid OwnerId { get; set; }

        public ICollection<Task> Tasks = new List<Task>();
    }
}
