namespace TaskManagement.DAL.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;

        public ICollection<Project> Projects { set; get; } = new List<Project>();
        public ICollection<Comment> Comments { set; get; } = new List<Comment>();
    }
}
