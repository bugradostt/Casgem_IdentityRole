using Casgem_IdentityRole.Dal;
using Casgem_IdentityRole.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Casgem_IdentityRole.Controllers
{
    public class LoginController : Controller
    {
        readonly SignInManager<AppUser> _signInManager;

        public LoginController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async  Task<IActionResult> Index(LoginViewModel p)
        {
            var result = await _signInManager.PasswordSignInAsync(p.UserName, p.Password,false,false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index","UserList");
            }

            return View();
        }
    }
}
