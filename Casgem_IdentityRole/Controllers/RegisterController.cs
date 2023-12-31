﻿using Casgem_IdentityRole.Dal;
using Casgem_IdentityRole.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Casgem_IdentityRole.Controllers
{
    public class RegisterController : Controller
    {
        readonly UserManager<AppUser> _userManager;

        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Index(RegisterViewModel model)
        {
            AppUser appUser = new AppUser()
            {
                NameSurname = model.NameSurname,
                Email = model.Mail,
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(appUser,model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Index","Login");
                
            }
            return View();
        }
    }
}
