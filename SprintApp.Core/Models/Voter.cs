namespace SprintApp.Core.Models
{
    public class Voter
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int VotedPoint { get; set; }
        public string? VoterId { get; set; }
        public string? ManagerId { get; set; }
        public ICollection<Vote>? Votes { get; set; }
    }
}
