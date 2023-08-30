using System.ComponentModel.DataAnnotations;

namespace DemoProject.Models
{
    public class School
    {
        [Key]
        public int SchoolId { get; set; }
        public string? Name { get; set; }
        public string? SchoolCode { get; set; }
        public List<Candidate>? Candidates { get; set; }
    }
}
