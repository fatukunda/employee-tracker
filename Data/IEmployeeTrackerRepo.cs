using System.Collections.Generic;
using employee_tracker.Models;

namespace employee_tracker.Data
{
    public interface IEmployeeTrackerRepo
    {
        bool SaveChanges();
        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployeeById(int id);
        void CreateEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
    }
}