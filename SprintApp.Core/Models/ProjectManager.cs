namespace SprintApp.Core.Models
{
    public class ProjectManager
    {
        int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailId { get; set; }
        public string? UserName { get; set; }
        public string? ManagerId { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public string? ResetToken { get; set; }
        public string? VerificationToken { get; set; }
        public int LoginAtempt { get; set; }
        public DateTime VerifiedAt { get; set; }
        public DateTime ResetTokenExpires { get; set; }
        public DateTime LogoutTime { get; set; }
        public DateTime VarificationTokenExpires { get; set;}
        public ICollection<Sprint> Sprints { get; set; } = new List<Sprint>();
        public ICollection<Voter>? Voters { get; set; }
        public ICollection<UserStory>? UserStories { get; set; }

    }
}
