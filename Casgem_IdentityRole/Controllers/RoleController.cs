using Casgem_IdentityRole.Dal;
using Casgem_IdentityRole.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Casgem_IdentityRole.Controllers
{
    public class RoleController : Controller
    {
        readonly UserManager<AppUser> _userManager;
        readonly RoleManager<AppRole> _roleManager;

        public RoleController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult ListUser()
        {
            var values = _userManager.Users.ToList();
            return View(values);
        }


        public IActionResult ListRole()
        {
            var values = _roleManager.Roles.ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult AddRole()
        {
           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(RoleAddViewModel p)
        {

            AppRole appRole = new AppRole()
            {
                Name = p.Name
            };
            var result = await _roleManager.CreateAsync(appRole);
            if(result.Succeeded)
            {
                return RedirectToAction("ListRole");
            }
            return View();
        }

        public async Task<IActionResult> DeleteRole(int id)
        {

           
            var result =  _roleManager.Roles.FirstOrDefault(x=>x.Id == id);
            await _roleManager.DeleteAsync(result);
            return RedirectToAction("RoleList");
        
        
        }


        [HttpGet]
        public async  Task<IActionResult> EditRole(int id)
        {
            var result = _roleManager.Roles.FirstOrDefault(x => x.Id == id);
            RoleEditViewModel model = new RoleEditViewModel()
            {
                id = result.Id,
                Name = result.Name
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(RoleEditViewModel p)
        {

            var value = _roleManager.Roles.FirstOrDefault(x => x.Id == p.id);
            value.Name = p.Name;
            await _roleManager.UpdateAsync(value);
            return RedirectToAction("ListRole");
        }

        [HttpGet]
        public async Task<IActionResult> AssignRole(int id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x =>x.Id==id);
            TempData["userId"] = user.Id;
            var roles = _roleManager.Roles.ToList();
            var userRoles = await _userManager.GetRolesAsync(user);
            List<RoleAssignViewModel> roleAssignViewModel = new List<RoleAssignViewModel>();
            foreach (var i in roles)
            {
                RoleAssignViewModel model = new RoleAssignViewModel();
                model.RoleId = i.Id; ;
                model.RoleName= i.Name;
                model.RoleExist = userRoles.Contains(i.Name);
                roleAssignViewModel.Add(model);
                
            }
            return View(roleAssignViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> AssignRole(List<RoleAssignViewModel> model)
        {
            var userId = (int)TempData["userId"];
            var user = _userManager.Users.FirstOrDefault(x => x.Id == userId);

            foreach (var i in model)
            {
                if (i.RoleExist)
                {
                    await _userManager.AddToRoleAsync(user, i.RoleName);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, i.RoleName);
                }
            }
            return RedirectToAction("ListUser");



        }



    }
}
