using DemoProject.Models.ViewModels;
using DemoProject.Models;

namespace DemoProject.Services.Abstraction
{
    public interface IRegistrationService
    {
        School SubmitCode(SchoolViewModel schoolViewModel);
        Candidate RegisterCandidate(RegistrationViewModel candidateViewModel, int schoolId);
        CompletedViewModel GenerateCompletedViewModel(int candidateId);
        School GetSchoolByCode(string schoolCode);
        string GenerateCandidateID(int candidateId);
    }
}
