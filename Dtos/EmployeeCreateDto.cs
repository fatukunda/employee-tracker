using System.ComponentModel.DataAnnotations;

namespace employee_tracker.Dtos
{
    public class EmployeeCreateDto
    {
        [Required]
        public string name { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string category { get; set; }
        [Required]
        public string salaryScale { get; set; }
    }
}