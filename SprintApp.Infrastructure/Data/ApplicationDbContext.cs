using Microsoft.EntityFrameworkCore;
using SprintApp.Core.Models;

namespace SprintApp.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<ProjectManager> ProjectManagers { get; set; }
        public DbSet<Voter> Voters { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Sprint> Sprints { get; set; }
        public DbSet<UserStory> UserStorys { get; set; }
    }
}
