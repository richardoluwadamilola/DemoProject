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
            // Create a dictionary to store candidates grouped by school code
            var candidatesBySchool = new Dictionary<string, List<OnboardedCandidatesViewModel>>();

            // Fetch candidates with school codes using the modified query
            var schoolCodes = _context.Schools.Select(s => s.SchoolCode).Distinct().ToList();

            foreach (var schoolCode in schoolCodes)
            {
                var candidates = _registrationService.GetOnboardedCandidates(schoolCode);
                candidatesBySchool.Add(schoolCode, candidates);
            }

            // Pass the dictionary to the view for display
            return View(candidatesBySchool);
        }

    }
}
