using SprintApp.Core.IRepositories;
using SprintApp.Core.Models;
using SprintApp.Infrastructure.Data;

namespace SprintApp.Infrastructure.Repositories
{
    public class UserStoryRepo : GenericRepo<UserStory>, IUserStoryRepo
    {
        public UserStoryRepo(ApplicationDbContext options) : base(options)
        {
            
        }
    }
}
