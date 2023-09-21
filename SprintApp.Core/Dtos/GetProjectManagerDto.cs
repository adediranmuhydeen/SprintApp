using SprintApp.Core.Models;

namespace SprintApp.Core.Dtos
{
    public class GetProjectManagerDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string EmailId { get; set; } = string.Empty;
        public string? UserName { get; set; }
        public ICollection<Sprint>? Sprints { get; set; } = new List<Sprint>();
        public ICollection<Voter>? Voters { get; set; } = new List<Voter>();
        public ICollection<UserStory>? UserStories { get; set; } = new List<UserStory>();
    }
}
