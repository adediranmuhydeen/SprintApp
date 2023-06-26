using SprintApp.Core.IRepositories;
using SprintApp.Core.Models;
using SprintApp.Infrastructure.Data;

namespace SprintApp.Infrastructure.Repositories
{
    public class SprintRepo : GenericRepo<Sprint>, ISprintRepo
    {
        public SprintRepo(ApplicationDbContext options) : base(options)
        {
            
        }
    }
}
