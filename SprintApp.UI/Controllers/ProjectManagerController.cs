using Microsoft.AspNetCore.Mvc;
using SprintApp.Core.Dtos;
using SprintApp.Core.Helper;
using SprintApp.Core.IServices;
using SprintApp.Core.Models;
#nullable disable

namespace SprintApp.UI.Controllers
{
    public class ProjectManagerController : Controller
    {
        private readonly IProjectManagerService _service;

        public ProjectManagerController(IProjectManagerService service)
        {
            _service = service;
        }

        public async Task<ActionResult<ProjectManager>> ProjectManagerLandingPage(string email)
        {
            
            var result = await _service.GetProjectManagerAsync(email);
            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }

        [HttpPost]
        public IActionResult ProjectManagerLandingPage()
        {
            
            return View();
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
        public async Task<ActionResult<LoginDto>> Login(LoginDto dto)
        {
            var result = await _service.Login(dto);
            if (result == ConstantMessage.TokenExpired)
            {
                return RedirectToAction("Verify");
            }
            if (result == ConstantMessage.CompleteRequest)
            {
                return View("ProjectManagerLandingPage");
            }
            return RedirectToAction("Home/Index");
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
            if( result == ConstantMessage.InvalidToken)
            {
                return RedirectToAction("Verify");
            }
            if (result != ConstantMessage.CompleteRequest)
            {
                return BadRequest(result);
            }
            return RedirectToAction("Login");
        }
    }
}
