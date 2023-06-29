using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SprintApp.Core.Models
{
    public class ProjectManager
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailId { get; set; } = string.Empty;
        public string? UserName { get; set; }
        public string ManagerId { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];
        public string? ResetToken { get; set; }
        public string? VerificationToken { get; set; }
        public int? LoginAtempt { get; set; }
        public DateTime? VerifiedAt { get; set; }
        public DateTime? ResetTokenExpires { get; set; }
        public DateTime? LogoutTime { get; set; }
        public DateTime? VarificationTokenExpires { get; set;}
        public ICollection<Sprint>? Sprints { get; set; } = new List<Sprint>();
        public ICollection<Voter>? Voters { get; set; } = new List<Voter>();
        public ICollection<UserStory>? UserStories { get; set; } = new List<UserStory>();

    }
}
