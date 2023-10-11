using SprintApp.Core.Models;

namespace SprintApp.Core.Dtos
{
    public class GetSprintDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? SprintId { get; set; }
        public string? ManagerId { get; set; }
        public ICollection<UserStory>? UserStories { get; set; }
        public ICollection<Vote>? Votes { get; set; }
    }
}
