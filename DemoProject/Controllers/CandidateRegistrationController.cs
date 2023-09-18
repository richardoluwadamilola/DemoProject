using DemoProject.Models;
using DemoProject.Models.ViewModels;
using DemoProject.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DemoProject.Controllers
{
    public class CandidateRegistrationController : Controller
    {
        private readonly IRegistrationService _registrationService;

        public CandidateRegistrationController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpGet]
        public IActionResult Index(string schoolCode)
        {
            // Get school based on provided school code
            School school = _registrationService.GetSchoolByCode(schoolCode);

            if (school == null)
            {
                return RedirectToAction("SchoolCodeNotFound");
            }

            // Pass the school ID to the view
            return View(school);
        }

        [HttpPost]
        public IActionResult RegisterCandidate(RegistrationViewModel candidateViewModel)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the school based on the entered school code
                School school = _registrationService.GetSchoolByCode(candidateViewModel.SchoolCode);

                if (school == null)
                {
                    ModelState.AddModelError("SchoolCode", "School with the provided code does not exist.");
                    return View("Index", candidateViewModel);
                }

                // Now you have the school ID, use it to associate the candidate
                CompletedViewModel registeredCandidate = _registrationService.RegisterCandidate(candidateViewModel, school.SchoolId);

                // Convert the passport image to Base64 and assign it to the view model
                if (candidateViewModel.Passport != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        candidateViewModel.Passport.CopyTo(memoryStream);
                        var bytes = memoryStream.ToArray();
                        var base64 = Convert.ToBase64String(bytes);
                        registeredCandidate.PassportBase64 = base64;
                    }
                }
                registeredCandidate.SchoolCode = school.SchoolCode;

                return View("Confirmation", registeredCandidate);
            }

            return View("Index");
        }

        [HttpGet]
        public IActionResult SchoolCodeNotFound()
        {
            return View();
        }
    }
}
