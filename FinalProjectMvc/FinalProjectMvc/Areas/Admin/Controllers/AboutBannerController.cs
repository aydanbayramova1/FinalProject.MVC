using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.AboutBanner;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutBannerController : Controller
    {
        private readonly IAboutBannerService _aboutBannerService;

        public AboutBannerController(IAboutBannerService aboutBannerService)
        {
            _aboutBannerService = aboutBannerService;
        }

        public async Task<IActionResult> Index()
        {
            var vmList = await _aboutBannerService.GetAllAsync();
            ViewBag.CanCreate = vmList.Count == 0;
            return View(vmList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AboutBannerCreateVM model)
        {
            if (!ModelState.IsValid) return View(model);

            await _aboutBannerService.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var banner = await _aboutBannerService.GetByIdAsync(id);
            if (banner == null) return NotFound();

            var vm = new AboutBannerEditVM
            {
                Id = banner.Id,
                Title = banner.Title,
                Img = banner.Img
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AboutBannerEditVM model)
        {
            if (!ModelState.IsValid) return View(model);

            await _aboutBannerService.EditAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int id)
        {
            var banner = await _aboutBannerService.GetByIdAsync(id);
            if (banner == null) return NotFound();

            var vm = new AboutBannerDetailVM
            {
                Title = banner.Title,
                Img = banner.Img
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _aboutBannerService.DeleteAsync(id);
            return Ok(); 
        }
    }
}
