using SprintApp.Core.IRepositories;
using SprintApp.Core.Models;
using SprintApp.Infrastructure.Data;

namespace SprintApp.Infrastructure.Repositories
{
    public class ProjectManagerRepo : GenericRepo<ProjectManager>, IProjectManagerRepo
    {
        public ProjectManagerRepo(ApplicationDbContext options) : base(options)
        {

        }
    }
}
