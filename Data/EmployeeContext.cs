using employee_tracker.Models;
using Microsoft.EntityFrameworkCore;

namespace employee_tracker.Data
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> opt ) : base(opt)
        {
            
        }
        public DbSet<Employee> Employees { get; set; }
    }
}