using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TestingMVC.Models
{
    public class MokeEmployeeRepository : IEmployeeRepository
    {
        private readonly List<Employee> _employeeList;

        public MokeEmployeeRepository()
        {
            _employeeList= new List<Employee>()
            {
                new Employee(){Id = 1, Name = "Jabrail", Department = "IT", Email = "Jabrail@mail.com"},
                new Employee(){Id = 2, Name = "Nigar", Department = "HR", Email = "Nigar@mail.com"},
                new Employee(){Id = 3, Name = "Narmin", Department = "B", Email = "Narmin@mail.com"},
                new Employee(){Id = 4, Name = "Ibrahim", Department = "Ms", Email = "Ibrahim@mail.com"},
                new Employee(){Id = 5, Name = "Resul", Department = "SOS", Email = "Resul@mail.com"}

            };
        }
        public Employee GetEmployee(int id)
        {
            return _employeeList.FirstOrDefault(x =>true);
        }



        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employeeList;
        }
    }
}
