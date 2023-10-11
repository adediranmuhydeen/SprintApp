using Microsoft.AspNetCore.Mvc;
using SprintApp.Core.Dtos;
using SprintApp.Core.IServices;
using SprintApp.Core.Models;

namespace SprintApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectManagerController : ControllerBase
    {
        private readonly IProjectManagerService _service;

        public ProjectManagerController(IProjectManagerService service)
        {
            _service = service;
        }

        [HttpPost("RegisterManager")]
        public async Task<IActionResult> Register(RegistrationDto dto)
        {
            return Ok(_service.RegisterManager(dto));
        }

        [HttpGet("ManagerLogin")]
        public async Task<ActionResult<ProjectManager>> ManagerLogin(LoginDto dto)
        {
            return Ok(_service.Login(dto));
        }

        [HttpGet("VerifyUser")]
        public async Task<IActionResult> Verification(VerificationDto dto)
        {
            return Ok(_service.VerifyUser(dto));
        }

        [HttpGet("GetProjectManager")]
        public async Task<ActionResult<GetProjectManagerDto>> GetProjectManager(string email)
        {
            return Ok(_service.GetProjectManagerAsync(email));
        }
    }
}
