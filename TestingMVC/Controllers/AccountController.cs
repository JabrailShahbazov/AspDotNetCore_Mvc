﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestingMVC.Models;
using TestingMVC.ViewModels;

namespace TestingMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        //GET: /<controller>/
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Going Registration 
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    City = model.City
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                //Register Succeeded
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    //Note: First Parameter Action and second parameter Controller
                    return RedirectToAction("Index", "Home");
                }
                //if problem for registration Error
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }




        //GET: /<controller>/
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password,
                    model.RememberMe, false);
                if (result.Succeeded)
                {
                    //For Security
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid LogIn Attempt");
            }
            return View(model);
        }

        //Remote Email Validation
        [AcceptVerbs("Get","Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var usr = await _userManager.FindByEmailAsync(email);
            if (usr==null)
            {
                return Json(true);   
            }
            else
            {
                return Json($"{email} is already in use");
            }
        }
    }
}