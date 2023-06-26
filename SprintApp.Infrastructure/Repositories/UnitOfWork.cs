using SprintApp.Core.IRepositories;
using SprintApp.Infrastructure.Data;

namespace SprintApp.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IVoteRepo voteRepo => throw new NotImplementedException();

        public IVoterRepo voterRepo => throw new NotImplementedException();

        public IProjectManagerRepo projectManagerRepo => throw new NotImplementedException();

        public ISprintRepo printRepo => throw new NotImplementedException();

        public IUserStoryRepo userStoryRepo => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
