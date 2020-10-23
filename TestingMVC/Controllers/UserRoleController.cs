using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestingMVC.ViewModels;

namespace TestingMVC.Controllers
{
    public class UserRoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        [HttpGet]
        public IActionResult CreateUserRole()
        {
            return View();
        }

        //POST: /<controller>/CreateUserRole
        [HttpPost]
        public async Task<IActionResult> CreateUserRole(UserRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole =new IdentityRole
                {
                    Name = model.RoleName
                };
                IdentityResult result =  await _roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "UserRole");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("",error.Description);
                }
            }

            return View(model);
        }
        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }
    }
}
