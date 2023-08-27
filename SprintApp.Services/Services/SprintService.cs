using SprintApp.Core.Dtos;
using SprintApp.Core.IRepositories;
using SprintApp.Core.IServices;

namespace SprintApp.Services.Services
{
    public class SprintService : ISprintService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SprintService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //public async Task<string> AddSprint(SprintDto dto)
        //{

        //}
    }
}
