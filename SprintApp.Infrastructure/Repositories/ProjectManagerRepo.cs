using SprintApp.Core.IRepositories;
using SprintApp.Core.Models;
using SprintApp.Infrastructure.Data;

namespace SprintApp.Infrastructure.Repositories
{
    public class ProjectManagerRepo : GenericRepo<ProjectManager>, IProjectManagerRepo
    {
        private readonly ApplicationDbContext _context;
        public ProjectManagerRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
