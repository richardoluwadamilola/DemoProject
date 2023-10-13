using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace DemoProject.Models
{
    public class Candidate
    {
        [Required]
        public int CandidateId { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? MiddleName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? Gender { get; set; }
        [Required]
        public DateTime DOB { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Category { get; set; }
        public string? PassportExtension { get; set; }
        [Required]
        public byte[]? Passport { get; set; }

        [ForeignKey("School")]
        public int SchoolId { get; set; }
        public School? School { get; set; }
    }
}
