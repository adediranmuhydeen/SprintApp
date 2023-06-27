using SprintApp.Core.IRepositories;
using SprintApp.Infrastructure.Data;

namespace SprintApp.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Private Properties
        protected IVoterRepo _voterRepo;
        protected IVoteRepo _voteRepo;
        protected IProjectManagerRepo _projectManagerRepo;
        protected ISprintRepo _sprintRepo;
        protected IUserStoryRepo _userStoryRepo;
        private readonly ApplicationDbContext _context;
        private bool _disposed;
        #endregion Private Properties

        #region Constructor
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion Constructor


        #region Public Properties
        public IVoteRepo voteRepo => _voteRepo?? new VoteRepo(_context);

        public IVoterRepo voterRepo => _voterRepo?? new VoterRepo(_context);

        public IProjectManagerRepo projectManagerRepo => _projectManagerRepo?? new ProjectManagerRepo(_context);

        public ISprintRepo sprintRepo => _sprintRepo?? new SprintRepo(_context);

        public IUserStoryRepo userStoryRepo => _userStoryRepo?? new UserStoryRepo(_context);

        #endregion Public Properties

        #region Public Method
        public void Dispose()
        {
            if (_disposed)
            {
                _context.Dispose();
                GC.SuppressFinalize(this);
            }
            
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        #endregion Public Method
    }
}
