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
using System.Data;

namespace DemoProject.Services.Implementation
{
    public class RegistrationService : IRegistrationService
    {
        private readonly DemoProjectContext _context;

        public RegistrationService(DemoProjectContext context)
        {
            _context = context;
        }

        private CompletedViewModel GetLatestCandidateBySchoolId(RegistrationViewModel regVM)
        {
            string sqlQuery = "SELECT TOP 1 * FROM Candidates " +
                  "WHERE FirstName = @FirstName AND LastName = @LastName AND MiddleName = @MiddleName AND DOB = @DateOfBirth";

            var candidate = _context.Candidates
                .FromSqlRaw(sqlQuery,
                    new SqlParameter("@FirstName", regVM.FirstName),
                    new SqlParameter("@LastName", regVM.Surname),
                    new SqlParameter("@MiddleName", regVM.MiddleName),
                    new SqlParameter("@DateOfBirth", regVM.DateOfBirth))
                .FirstOrDefault();

            return GenerateCompletedViewModel(candidate);


        }

        public School SubmitCode(SchoolViewModel schoolViewModel)
        {
            var schoolCode = _context.Schools.FirstOrDefault(c => c.SchoolCode == schoolViewModel.SchoolCode);

            if (schoolCode == null)
            {
                throw new Exception("The entered school code does not exist.");
            }

            return schoolCode;
        }

        public School GetSchoolByCode(string schoolCode)
        {
            string sqlQuery = "SELECT TOP 1 * FROM Schools WHERE SchoolCode = @SchoolCode";

            var school = _context.Schools
                .FromSqlRaw(sqlQuery, new SqlParameter("@SchoolCode", schoolCode))
                .FirstOrDefault();

            return school;

        }

        public CompletedViewModel RegisterCandidate(RegistrationViewModel candidateViewModel, int schoolId)
        {
            // Check if the school has reached the limit of 10 students
            int registeredStudents = _context.Candidates.Count(c => c.SchoolId == schoolId);

            if (registeredStudents >= 10)
            {
                // Handle the case where the limit is reached by returning a custom error message
                throw new Exception("The maximum limit of 10 students for this school has been reached.");
            }

            byte[] passport = null;
            string extension = null;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                candidateViewModel?.Passport?.CopyTo(memoryStream);
                passport = memoryStream.ToArray();
                extension = Path.GetExtension(candidateViewModel?.Passport?.FileName);
            }

            // Build the SQL query with parameters
            string insertCandidateQuery =
                "INSERT INTO Candidates (FirstName, MiddleName, LastName, DOB, Gender, Category, SchoolId, Passport, PassportExtension) " +
                "VALUES (@FirstName, @MiddleName, @LastName, @DOB, @Gender, @Category, @SchoolId, @Passport, @PassportExtension)";

            // Create parameters for the query
            var parameters = new List<SqlParameter>
    {
        new SqlParameter("@FirstName", candidateViewModel.FirstName),
        new SqlParameter("@MiddleName", candidateViewModel.MiddleName),
        new SqlParameter("@LastName", candidateViewModel.Surname),
        new SqlParameter("@DOB", candidateViewModel.DateOfBirth),
        new SqlParameter("@Gender", candidateViewModel.Gender),
        new SqlParameter("@Category", candidateViewModel.Category),
        new SqlParameter("@SchoolId", schoolId),
        new SqlParameter("@Passport", SqlDbType.VarBinary) { Value = passport },
        new SqlParameter("@PassportExtension", extension) // Use extension here
    };

            // Execute the query with parameters
            _context.Database.ExecuteSqlRaw(insertCandidateQuery, parameters.ToArray());

            return GetLatestCandidateBySchoolId(candidateViewModel);
        }



        public CompletedViewModel GenerateCompletedViewModel(Candidate candidate)
        {
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
