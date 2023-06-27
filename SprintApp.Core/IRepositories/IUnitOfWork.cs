namespace SprintApp.Core.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IVoteRepo voteRepo { get; }
        IVoterRepo voterRepo { get; }
        IProjectManagerRepo projectManagerRepo { get; }
        ISprintRepo sprintRepo { get; }
        IUserStoryRepo userStoryRepo { get; }
        Task SaveChangesAsync();
    }
}
