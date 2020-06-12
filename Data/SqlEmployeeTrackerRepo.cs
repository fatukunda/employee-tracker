using System;
using System.Collections.Generic;
using System.Linq;
using employee_tracker.Models;

namespace employee_tracker.Data
{
    public class SqlEmployeeTrackerRepo : IEmployeeTrackerRepo
    {
        private readonly EmployeeContext _context;

        public SqlEmployeeTrackerRepo(EmployeeContext context)
        {
            _context = context;
        }

        public void CreateEmployee(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }
            _context.Employees.Add(employee);
        }

        public void DeleteEmployee(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            };
            _context.Employees.Remove(employee);
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _context.Employees.ToList();
        }

        public Employee GetEmployeeById(int id)
        {
            return _context.Employees.FirstOrDefault(employee => employee.id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateEmployee(Employee employee)
        {
            // Taken care of by the DB context.
        }
    }
}