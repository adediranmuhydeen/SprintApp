using Microsoft.AspNetCore.Mvc;
using SprintApp.Core.Dtos;
using SprintApp.Core.Helper;
using SprintApp.Core.IServices;

namespace SprintApp.UI.Controllers
{
    public class ProjectManagerController : Controller
    {
        private readonly IProjectManagerService _service;

        public ProjectManagerController(IProjectManagerService service)
        {
            _service = service;
        }

        public IActionResult Register()
        {
            return View();
        }

        //Post
        [HttpPost]
        public async Task<IActionResult> Register(RegistrationDto dto)
        {
            var result = await _service.RegisterManager(dto);
            if(result == ConstantMessage.Unsuccessful)
            {
                return BadRequest(result);
            }
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        //Post
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _service.Login(dto);
            if (result == ConstantMessage.TokenExpired)
            {
                return RedirectToAction("Verify");
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Verify()
        {
            return View();
        }

        //Post
        [HttpPost]
        public async Task<IActionResult> Verify(VerificationDto dto)
        {
            var result = await _service.VerifyUser(dto);
            if (result != ConstantMessage.CompleteRequest)
            {
                return BadRequest(result);
            }
            return RedirectToAction("Login");
        }
    }
}
