using DemoProject.Models.ViewModels;
using DemoProject.Models;

namespace DemoProject.Services.Abstraction
{
    public interface IRegistrationService
    {
        School SubmitCode(SchoolViewModel schoolViewModel);
        CompletedViewModel RegisterCandidate(RegistrationViewModel candidateViewModel, int schoolId);
        School GetSchoolByCode(string schoolCode);
        string GenerateCandidateID(int candidateId);
    }
}
