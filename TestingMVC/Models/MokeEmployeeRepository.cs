//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Threading.Tasks;

//namespace TestingMVC.Models
//{
//    public class MokeEmployeeRepository : IEmployeeRepository
//    {
//        private readonly List<Employee> _employeeList;

//        public MokeEmployeeRepository()
//        {
//            _employeeList= new List<Employee>()
//            {
//                new Employee(){Id = 1, Name = "Jabrail", Department = Dept.IT, Email = "Jabrail@mail.com"},
//                new Employee(){Id = 2, Name = "Nigar", Department = Dept.HR, Email = "Nigar@mail.com"},
//                new Employee(){Id = 3, Name = "Resul", Department = Dept.Payroll, Email = "Resul@mail.com"}

//            };
//        }
//        public Employee GetEmployee(int id)
//        {
//            return _employeeList.FirstOrDefault(e=> e.Id == id);
//        }

//        public IEnumerable<Employee> GetAllEmployees()
//        {
//            return _employeeList;
//        }

//        public Employee Add(Employee employee)
//        {
//            employee.Id = _employeeList.Max(e => e.Id) + 1;
//            _employeeList.Add(employee);
//            return employee;
//        }

//        public Employee Update(Employee employeeChangers)
//        {
//            Employee employee = _employeeList.FirstOrDefault(e => e.Id == e.Id);
//            if (employee != null)
//            {
//                employee.Name = employeeChangers.Name;
//                employee.Email = employeeChangers.Email;
//                employee.Department = employeeChangers.Department;
//            }

//            return employee;
//        }

//        public Employee Delete(int id)
//        {
//            Employee employee =_employeeList.FirstOrDefault(e => e.Id == e.Id);
//            if (employee!=null)
//            {
//                _employeeList.Remove(employee);
//            }

//            return employee;
//        }
//    }
//}
