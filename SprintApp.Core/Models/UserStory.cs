namespace SprintApp.Core.Models
{
    public class UserStory
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? StoryId { get; set; }
        public string? Description { get; set; }
        public string? SprintId { get; set; }
        public string? ManagerId { get; set; }
        public ICollection<Voter>? Voters { get; set; }
    }
}
