using SprintApp.Core.IRepositories;
using SprintApp.Core.Models;
using SprintApp.Infrastructure.Data;

namespace SprintApp.Infrastructure.Repositories
{
    public class VoteRepo : GenericRepo<Vote>, IVoteRepo
    {
        public VoteRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}
