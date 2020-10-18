using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestingMVC.Models;

namespace TestingMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employee;

        public HomeController(IEmployeeRepository employeeRepository)
        {
            _employee = employeeRepository;
        }
        public ViewResult Details(int? id)
        {
            var emp = _employee.GetEmployee(id??1);
            return View(emp);
        }

        public IActionResult Index()
        {
            var model = _employee.GetAllEmployees();
            return View(model);
        }
    }
}
