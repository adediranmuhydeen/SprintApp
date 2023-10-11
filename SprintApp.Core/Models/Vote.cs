namespace SprintApp.Core.Models
{
    public class Vote
    {
        public Guid Id { get; set; }
        public int Value { get; set; }
        public string? VoterId { get; set; }
    }
}