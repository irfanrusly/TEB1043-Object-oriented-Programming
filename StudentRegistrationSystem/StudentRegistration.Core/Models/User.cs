namespace StudentRegistration.Core.Models
{
    public class User
    {
        public string UserId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Role UserRole { get; set; }
    }
}
