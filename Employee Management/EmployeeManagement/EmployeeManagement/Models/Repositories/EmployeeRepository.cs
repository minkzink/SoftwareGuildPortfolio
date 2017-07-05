using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagement.Models
{
    public class EmployeeRepository
    {
        private static List<Employee> _employees;

        static EmployeeRepository()
        {
            _employees = new List<Employee>()
            {
                new Employee()
                {
                    EmployeeId = 1,
                    FirstName = "Jenny",
                    LastName = "Jones",
                    Phone = "888-218-5458",
                    DepartmentId = 1
                },
                new Employee()
                {
                    EmployeeId = 2,
                    FirstName = "Bobby",
                    LastName = "Ross",
                    Phone = "555-555-5555",
                    DepartmentId = 1
                },
                new Employee()
                {
                    EmployeeId = 4,
                    FirstName = "Mohit",
                    LastName = "Auraea",
                    Phone = "878-515-5656",
                    DepartmentId = 1
                },
                new Employee()
                {
                    EmployeeId = 5,
                    FirstName = "Donni",
                    LastName = "Darko",
                    Phone = "330-486-4888",
                    DepartmentId = 1
                },
                new Employee()
                {
                EmployeeId = 3,
                FirstName = "Lucy",
                LastName = "Turia",
                Phone = "485-220-2016",
                DepartmentId = 1
            }
            };
        }

        public static void Add(Employee employee)
        {
            if (_employees.Any())
            {
                employee.EmployeeId = _employees.Max(e => e.EmployeeId) + 1;
            }
            else
            {
                employee.EmployeeId = 1;
            }

            _employees.Add(employee);
        }

        public static void Edit(Employee employee)
        {
            _employees.RemoveAll(e => e.EmployeeId == employee.EmployeeId);
            _employees.Add(employee);
        }

        public static void Delete(int employeeId)
        {
            _employees.RemoveAll(e => e.EmployeeId == employeeId);
        }

        public static Employee Get(int employeeId)
        {
            return _employees.FirstOrDefault(e => e.EmployeeId == employeeId);
        }

        public static List<Employee> GetAll()
        {
            return _employees;
        }
    }
}