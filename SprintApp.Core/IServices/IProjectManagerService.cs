using SprintApp.Core.Dtos;
using SprintApp.Core.Models;

namespace SprintApp.Core.IServices
{
    public interface IProjectManagerService 
    {
        Task<string> RegisterManager(RegistrationDto obj);
        Task<string> Login(LoginDto dto);
        Task<string> VerifyUser(VerificationDto dto);
        Task<GetProjectManagerDto> GetProjectManagerAsync(string Email);
    }
}
