using DemoProject.Models;
using DemoProject.Models.ViewModels;
using DemoProject.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DemoProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRegistrationService _registrationService;

        public HomeController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var schoolViewModel = new SchoolViewModel();
            return View(schoolViewModel);
        }

        [HttpPost]
        public IActionResult SubmitCode(SchoolViewModel schoolViewModel)
        {
            try
            {
                var school = _registrationService.SubmitCode(schoolViewModel);
                return RedirectToAction("Index", "CandidateRegistration", school);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("SchoolCode", ex.Message);
                ViewData["ErrorMessage"] = ex.Message;
            }

            return View("Index", schoolViewModel);
        }

    }
}
