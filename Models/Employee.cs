using System.ComponentModel.DataAnnotations;

namespace employee_tracker.Models
{
    public class Employee
    {
        public int id { get; set; }
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