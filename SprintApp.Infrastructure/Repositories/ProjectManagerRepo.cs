using SprintApp.Core.IRepositories;
using SprintApp.Core.Models;
using SprintApp.Infrastructure.Data;

namespace SprintApp.Infrastructure.Repositories
{
    public class ProjectManagerRepo : GenericRepo<ProjectManager>, IProjectManagerRepo
    {
        private readonly ApplicationDbContext _context;
        public ProjectManagerRepo(ApplicationDbContext options, ApplicationDbContext context) : base(options)
        {
            _context = context;
        }
    }
}
