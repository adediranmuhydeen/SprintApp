﻿namespace SprintApp.Core.Models
{
    public class Sprint
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? SprintId { get; set; }
        public string? ManagerId { get; set; }
        public ICollection<UserStory>? UserStories { get; set; }
        public ICollection<Voter>? Voters { get; set;}
    }
}
