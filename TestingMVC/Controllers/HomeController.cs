using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestingMVC.Models;
using TestingMVC.ViewModels;

namespace TestingMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employee;
        private readonly IWebHostEnvironment _hostEnvironment;

        //Created IWebHostEnvironment For wwwroot mapping
        public HomeController(IEmployeeRepository employeeRepository, IWebHostEnvironment hostEnvironment)
        {
            _employee = employeeRepository;
            _hostEnvironment = hostEnvironment;
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

        public IActionResult Create()
        {
            return View();
        }  

        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.Photos!=null && model.Photos.Count>0)
                {
                    foreach (IFormFile photo in model.Photos)
                    {
                        string uploadFolder = Path.Combine(_hostEnvironment.WebRootPath, "images");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                        string filePath = Path.Combine(uploadFolder, uniqueFileName);
                        photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    }
                }
                Employee newEmployee = new Employee
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPat = uniqueFileName
                };
                _employee.Add(newEmployee);
                return RedirectToAction("Details", new { id = newEmployee.Id });
            }

            return View();
        }
    }
}
