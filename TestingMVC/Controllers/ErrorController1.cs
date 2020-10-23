using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TestingMVC.Controllers
{
    public class ErrorController1 : Controller
    {
        [Route("/Error/{StatusCode}")]
        public IActionResult Error(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
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
