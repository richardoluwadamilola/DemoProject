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
            var registrationViewModel = new RegistrationViewModel
            {
                // Pass the schoolId to the view
                SchoolId = schoolId
            };
            return View(registrationViewModel);
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
                Candidate registeredCandidate = _registrationService.RegisterCandidate(candidateViewModel, school.SchoolId);
                
                
            }
            // Display the confirmation page
            return View("Confirmation", "CandidateRegistration");
        }

        //public IActionResult Confirmation(int candidateId)
        //{
        //    var completedViewModel = _registrationService.GenerateCompletedViewModel(candidateId);
        //    return View(completedViewModel);
        //}
    }

}

