using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace DemoProject.Models
{
    public class Candidate
    {
        public int CandidateId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public DateTime DOB { get; set; }
        public string? Category { get; set; }
        public string? PassportExtension { get; set; }
        public byte[]? Passport { get; set; }

        [ForeignKey("School")]
        public int SchoolId { get; set; }
        public School? School { get; set; }
    }
}
