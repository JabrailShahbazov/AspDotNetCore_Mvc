using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestingMVC.Models;
using TestingMVC.ViewModels;

namespace TestingMVC.Controllers
{   [Authorize]
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

        [AllowAnonymous]
        //GET: Employee Details
        public ViewResult Details(int? id)
        {
            Employee employee = _employee.GetEmployee(id.Value);
            if (employee == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound",id.Value);
            }
            return View(employee);
        }

        [AllowAnonymous]
        //GET: All Employees
        public IActionResult Index()
        {
            var model = _employee.GetAllEmployees();
            return View(model);
        }

        //If Click Edit Button 
        //GET: /Edit/Id
        public IActionResult Edit(int id)
        {
            Employee employee = _employee.GetEmployee(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistingPhotoPath = employee.PhotoPat
            };
            return View(employeeEditViewModel);
        }


        //If you Edited Employee
        //POST:
        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = _employee.GetEmployee(model.Id);
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;
                if (model.Photos != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(_hostEnvironment.WebRootPath, "images"
                            , model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    employee.PhotoPat = ProcessUploadFile(model);

                }

                _employee.Update(employee);
                return RedirectToAction("Index");
            }

            return View();
        }

        //GET: /Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadFile(model);
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

        //Work on photo
        private string ProcessUploadFile(EmployeeCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photos != null && model.Photos.Count > 0)
            {
                foreach (IFormFile photo in model.Photos)
                {
                    string uploadFolder = Path.Combine(_hostEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);
                    }
                }
            }
            return uniqueFileName;
        }
   
    }
}
