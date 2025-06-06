using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.Setting;
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

        public async Task<IActionResult> Index()
        {
            var setting = await _settingService.GetSettingAsync();
            if (setting == null)
                return RedirectToAction(nameof(Create));

            return View(setting);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
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

        public async Task<IActionResult> Edit()
        {
            var setting = await _settingService.GetSettingAsync();
            if (setting == null)
                return RedirectToAction(nameof(Create));

            var editVm = new SettingEditVM
            {
                HeaderLogo = setting.HeaderLogo,
                FooterLogo = setting.FooterLogo,
                BackgroundImage = setting.BackgroundImage,
                Address = setting.Address,
                Email = setting.Email,
                Phone = setting.Phone,
                EverydayWorkingHours = setting.EverydayWorkingHours,
                KitchenCloseTime = setting.KitchenCloseTime
            };

            return View(editVm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SettingEditVM model)
        {
            if (!ModelState.IsValid)
                return View(model); 

            await _settingService.UpdateAsync(model);

            return RedirectToAction(nameof(Index));  
        }
        public async Task<IActionResult> Detail()
        {
            var setting = await _settingService.GetSettingAsync();
            if (setting == null)
                return NotFound();

            return View(setting);
        }

        [HttpPost]
        public async Task<IActionResult> Delete()
        {
            await _settingService.DeleteAllAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
