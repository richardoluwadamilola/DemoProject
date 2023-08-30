using DemoProject.Models;
using DemoProject.Models.ViewModels;
using DemoProject.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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
        public IActionResult Index(int schoolId)
        {
            var registrationViewModel = new RegistrationViewModel();
            // Pass the schoolId to the view
            ViewBag.SchoolId = schoolId;
            return View(registrationViewModel);
        }

        [HttpPost]
        public IActionResult RegisterCandidate(RegistrationViewModel candidateViewModel, int schoolId)
        {
            if (ModelState.IsValid)
            {
                var candidate = _registrationService.RegisterCandidate(candidateViewModel, schoolId);

                // Generate the unique candidate ID
                string candidateID = _registrationService.GenerateCandidateID(candidate.CandidateId);

                var completedViewModel = new CompletedViewModel
                {
                    FullName = $"{candidate.FirstName} {candidate.LastName}",
                    ID = candidateID,
                    Category = candidate.Category
                };

                // Display the confirmation page
                return View("Confirmation", "CandidateRegistration");
            }

            // If ModelState is not valid, return back to the registration page
            ViewBag.SchoolId = schoolId; // Pass the schoolId again to the view
            return View("RegisterCandidates", candidateViewModel);
        }

        //public IActionResult Confirmation(int candidateId)
        //{
        //    var completedViewModel = _registrationService.GenerateCompletedViewModel(candidateId);
        //    return View(completedViewModel);
        //}
    }

}

