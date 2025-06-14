using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.Setting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingController : Controller
    {
        private readonly ISettingService _settingService;

        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Index()
        {
            var setting = await _settingService.GetSettingAsync();
            if (setting == null)
                return RedirectToAction(nameof(Create));

            return View(setting);
        }


        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(SettingCreateVM model)
        {
            if (!ModelState.IsValid) return View(model);

            if (await _settingService.ExistsAsync())
            {
                ModelState.AddModelError("", "Settings already exist. Please edit the existing settings.");
                return View(model);
            }

            await _settingService.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }



        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Detail()
        {
            var setting = await _settingService.GetSettingAsync();
            if (setting == null)
                return NotFound();

            return View(setting);
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Edit()
        {
            var model = await _settingService.GetEditModelAsync();
            if (model == null) return NotFound();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Edit(SettingEditVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _settingService.EditAsync(model);
            return RedirectToAction(nameof(Index));
        }

    }
}
