using DemoProject.Models.ViewModels;
using DemoProject.Models;
using DemoProject.Services.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DemoProject.Data;
using Microsoft.Data.SqlClient;

namespace DemoProject.Services.Implementation
{
    public class RegistrationService : IRegistrationService
    {
        private readonly DemoProjectContext _context;

        public RegistrationService(DemoProjectContext context)
        {
            _context = context;
        }

        private Candidate GetLatestCandidateBySchoolId(int schoolId)
        {
            return _context.Candidates
                .Where(c => c.SchoolId == schoolId)
                .OrderByDescending(c => c.CandidateId)
                .FirstOrDefault();
        }

        public School SubmitCode(SchoolViewModel schoolViewModel)
        {
            var schoolCode = _context.Schools.FirstOrDefault(c => c.SchoolCode == schoolViewModel.SchoolCode);

            if (schoolCode == null)
            {
                throw new InvalidOperationException("The entered school code does not exist.");
            }

            return schoolCode;
        }

        public Candidate RegisterCandidate(RegistrationViewModel candidateViewModel, int schoolId)
        {
            string insertCandidateQuery = $"INSERT INTO Candidates (FirstName, MiddleName, LastName, DOB, Gender, Category, SchoolId) VALUES " +
                                          $"('{candidateViewModel.FirstName}', '{candidateViewModel.MiddleName}', '{candidateViewModel.Surname}', " +
                                          $"'{DateTime.Parse(candidateViewModel.DateOfBirth):yyyy-MM-dd}', '{candidateViewModel.Gender}', " +
                                          $"'{candidateViewModel.Category}', {schoolId})";

            using var command = _context.Database.GetDbConnection().CreateCommand();
            command.CommandText = insertCandidateQuery;
            command.ExecuteNonQuery();

            return GetLatestCandidateBySchoolId(schoolId);
        }




        public CompletedViewModel GenerateCompletedViewModel(int candidateId)
        {
            var candidate = GetCandidateById(candidateId);

            var completedViewModel = new CompletedViewModel
            {
                FullName = $"{candidate.FirstName} {candidate.LastName}",
                ID = GenerateCandidateID(candidate.CandidateId),
                Category = candidate.Category
            };

            return completedViewModel;
        }

        private Candidate GetCandidateById(int candidateId)
        {
            return _context.Candidates.FirstOrDefault(c => c.CandidateId == candidateId);
        }

        public string GenerateCandidateID(int candidateId)
        {
            string candidateIdString = candidateId.ToString().PadLeft(3, '0');
            string uniqueID = $"CP{candidateIdString}";
            return uniqueID;
        }

    }
}
