namespace TaskManagement.DAL.Entities
{
    public class Task : BaseEntity
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime DueDate { get; set; }
        public TaskStatus Status { get; set; } = null!;
        public Guid StatusId { get; set; }
        public Project Project { get; set; } = null!;
        public Guid ProjectId { get; set; }
        public int Priority { get; set; }
        public int Order { get; set; }

        public ICollection<Comment> Comments = new List<Comment>();
    }
}
