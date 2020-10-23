using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TestingMVC.Controllers
{
    public class ErrorController1 : Controller
    {
        [Route("/Error/{StatusCode}")]
        public IActionResult Error(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry the resource you requested could not be found";
                    break;
            }
            return View("NotFound");
        }
    }
}
