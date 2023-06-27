using Microsoft.AspNetCore.Mvc;
using SprintApp.Core.IServices;

namespace SprintApp.UI.Controllers
{
    public class ProjectManagerController : Controller
    {
        private readonly IProjectManagerService _service;
        public IActionResult Register()
        {
            return View();
        }
    }
}
