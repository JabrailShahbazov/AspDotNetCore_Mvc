using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TestingMVC.Models
{
   public interface IEmployeeRepository
   {
       Employee GetEmployee(int id);
       IEnumerable<Employee> GetAllEmployees();
       Employee Add(Employee employee);
       Employee Update(Employee employee);
       Employee Delete(int id);
   }
}
