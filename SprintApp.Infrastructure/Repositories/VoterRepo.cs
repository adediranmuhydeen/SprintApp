using SprintApp.Core.IRepositories;
using SprintApp.Core.Models;
using SprintApp.Infrastructure.Data;

namespace SprintApp.Infrastructure.Repositories
{
    public class VoterRepo : GenericRepo<Voter>, IVoterRepo
    {
        public VoterRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}
