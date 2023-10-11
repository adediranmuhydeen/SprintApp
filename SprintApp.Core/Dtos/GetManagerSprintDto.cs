using SprintApp.Core.Models;

namespace SprintApp.Core.Dtos
{
    public class GetManagerSprintDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? SprintId { get; set; }
        public ICollection<UserStory>? UserStories { get; set; }
        public ICollection<Voter>? Voters { get; set; }
    }
}
