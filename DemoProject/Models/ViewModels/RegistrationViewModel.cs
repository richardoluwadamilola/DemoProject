namespace DemoProject.Models.ViewModels
{
    public class RegistrationViewModel
    {
         public string? SchoolCode { get; set; }
        public string? SchoolName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? Surname { get; set; }
        public string? Gender { get; set; }
        public string? DateOfBirth { get; set; }
        public string? Category { get; set; }
        public IFormFile? Passport { get; set; }
    }
}
