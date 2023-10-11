using AutoMapper;
using SprintApp.Core.Dtos;
using SprintApp.Core.IRepositories;
using SprintApp.Core.IServices;
using SprintApp.Core.Models;

namespace SprintApp.Services.Services
{
    public class SprintService : ISprintService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SprintService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Public Methods
        public async Task<SprintDto> AddSprint(SprintDto dto, string managerGuid)
        {
            var projectManager = await _unitOfWork.projectManagerRepo.GetAsync(x => x.Id.ToString() == managerGuid);
            if (projectManager == null)
            {
                return new SprintDto();
            }

            List<Sprint> item = await _unitOfWork.sprintRepo.GetAllAsync();
            var myList = new List<string>();
            if (item == null)
            {
                myList.Add("SPTAA0001A");
            }
            foreach (var sprint in item)
            {
                myList.Add(sprint.SprintId);
            }

            var mySprint = new Sprint
            {
                Id = new Guid(),
                Title = dto.Title,
                Description = dto.Description,
                CreatedDate = DateTime.UtcNow,
                SprintId = await GenerateCode("SPT", myList, 3),
                ManagerId = projectManager.ManagerId
            };
            await _unitOfWork.sprintRepo.CreateAsync(mySprint);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<SprintDto>(mySprint);
        }

        public async Task<List<GetManagerSprintDto>> AddManySprint (List<Sprint> items, string managerGuid)
        {
            var projectManager = await _unitOfWork.projectManagerRepo.GetAsync(x => x.Id.ToString() == managerGuid);
            if (projectManager == null)
            {
                return new List<GetManagerSprintDto>();
            }

            List<Sprint> item = await _unitOfWork.sprintRepo.GetAllAsync();
            var myList = new List<string>();
            if (item == null)
            {
                myList.Add("SPTAA0001A");
            }
            foreach (var sprint in item)
            {
                myList.Add(sprint.SprintId);
            }
            var sprintList = new List<Sprint>();
            foreach(var dto in item)
            {
                var sprint = new Sprint
                {
                    Id = new Guid(),
                    Title = dto.Title,
                    Description = dto.Description,
                    CreatedDate = DateTime.UtcNow,
                    SprintId = await GenerateCode("SPT", myList, 3),
                    ManagerId = projectManager.ManagerId,
                };
                sprintList.Add(sprint);
            }
            
            if (sprintList.Count > 0 || sprintList == null)
            {
                return new List<GetManagerSprintDto>();
            }

            await _unitOfWork.sprintRepo.AddManyAsync(sprintList);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<List<GetManagerSprintDto>>(sprintList);
        }

        public async Task<List<GetManagerSprintDto>> GetSprintsForManager(string managerGuid)
        {
            if(!Guid.TryParse(managerGuid, out var manager))
            {
                return new List<GetManagerSprintDto>();
            }
            var projectManager = await _unitOfWork.projectManagerRepo.GetAsync(x => x.Id.ToString() == managerGuid);
            if(projectManager == null)
            {
                return new List<GetManagerSprintDto>();
            }
            var sprintList = await _unitOfWork.sprintRepo.GetAllAsync(x=> x.ManagerId == projectManager.ManagerId);
            if(sprintList == null)
            {
                return new List<GetManagerSprintDto>();
            }
            foreach (var sprint in sprintList)
            {
                sprint.UserStories = await _unitOfWork.userStoryRepo.GetAllAsync(x => x.SprintId == sprint.SprintId);
                sprint.Voters = await _unitOfWork.voterRepo.GetAllAsync(x => x.SPrintId == sprint.SprintId);
            }
            return _mapper.Map<List<GetManagerSprintDto>>(sprintList);
        }



        #endregion Public Methods

        #region Private Methods
        /// <summary>
        /// Method to generate User IDs
        /// </summary>
        /// <param name="name"></param>
        /// <param name="_service"></param>
        /// <param name="charLength"></param>
        /// <param name="firstIndex"></param>
        /// <param name="secondIndex"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        private async Task<string> GenerateCode(string name, List<string> _service, int charLength, int firstIndex = 2, int secondIndex = 3, int startIndex = 4)
        {

            if (charLength > 2)
            {
                startIndex = startIndex + (charLength - 2);
            }
            string myString = _service[_service.Count - 1];
            int num = int.Parse(myString.Substring(startIndex, 4));
            char firstChar = myString[firstIndex + (charLength - 2)];
            char secondChar = myString[secondIndex + (charLength - 2)];
            char thirdChar = myString[myString.Length - 1];
            if (num + 1 > 9999)
            {
                secondChar = (char)(secondChar + 1);
                num = 0;
                if (secondChar > 'Z')
                {
                    firstChar = (char)(firstChar + 1);
                    secondChar = (char)(secondChar - 26);

                    if (firstChar > 'Z')
                    {
                        firstChar = (char)(firstChar - 26);
                        thirdChar = (char)(thirdChar + 1);
                    }
                }
            }
            return (name.Substring(0, charLength).ToUpper() + firstChar + secondChar + (num + 1).ToString("D4") + thirdChar);
        }

        #endregion Private Methods
    }
}
