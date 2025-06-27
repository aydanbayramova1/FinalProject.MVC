using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IRoleService _roleService;

        public UserController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var roles = await _roleService.GetRolesAsync();
            var usernames = await _roleService.GetUsernamesAsync();

            var model = new ManageUserRolesVM
            {
                Roles = roles,
                Usernames = usernames
            };

            return View(model);
        }


        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(string username, string role)
        {
            var result = await _roleService.AddRoleAsync(username, role);
            TempData["Message"] = result;
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveRole(string username, string role)
        {
            var result = await _roleService.RemoveRoleAsync(username, role);
            TempData["Message"] = result;
            return RedirectToAction("Index");
        }
    }
}
