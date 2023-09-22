using DemoProject.Data;
using DemoProject.Models.ViewModels;
using DemoProject.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace DemoProject.Controllers
{
    public class OnboardedCandidatesController : Controller
    {
        private readonly IRegistrationService _registrationService;
        private readonly DemoProjectContext _context;

        public OnboardedCandidatesController(IRegistrationService registrationService, DemoProjectContext context)
        {
            _registrationService = registrationService;
            _context = context;
        }

        [HttpGet]
        [Route("candidates/byschoolcode")]
        public IActionResult Index()
        {
            // Create a dictionary to store candidates grouped by school code and school name
            var candidatesBySchool = new Dictionary<string, Dictionary<string, List<OnboardedCandidatesViewModel>>>();

            // Fetch school codes and school names using the modified query
            var schools = _context.Schools.ToList();

            foreach (var school in schools)
            {
                var schoolCode = school.SchoolCode;
                var schoolName = school.Name;

                // Fetch candidates for the current school using the school code and school name
                var candidates = _registrationService.GetOnboardedCandidates(schoolCode, schoolName);

                // Check if the school code exists in the dictionary
                if (!candidatesBySchool.ContainsKey(schoolCode))
                {
                    candidatesBySchool.Add(schoolCode, new Dictionary<string, List<OnboardedCandidatesViewModel>>());
                }

                // Add or update the candidates for the current school code and school name
                candidatesBySchool[schoolCode].Add(schoolName, candidates);
            }

            // Pass the dictionary to the view for display
            return View(candidatesBySchool);
        }


    }
}
