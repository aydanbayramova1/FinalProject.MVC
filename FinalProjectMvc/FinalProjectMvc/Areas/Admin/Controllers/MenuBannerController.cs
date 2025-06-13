using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.MenuBanner;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MenuBannerController : Controller
    {
        private readonly IMenuBannerService _menuBannerService;

        public MenuBannerController(IMenuBannerService menuBannerService)
        {
            _menuBannerService = menuBannerService;
        }

        public async Task<IActionResult> Index()
        {
            var vmList = await _menuBannerService.GetAllAsync();
            ViewBag.CanCreate = vmList.Count == 0;
            return View(vmList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MenuBannerCreateVM model)
        {
            if (!ModelState.IsValid) return View(model);

            await _menuBannerService.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var banner = await _menuBannerService.GetByIdAsync(id);
            if (banner == null) return NotFound();

            var vm = new MenuBannerEditVM
            {
                Id = banner.Id,
                Title = banner.Title,
                Img = banner.Img
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MenuBannerEditVM model)
        {
            if (!ModelState.IsValid) return View(model);

            await _menuBannerService.EditAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int id)
        {
            var banner = await _menuBannerService.GetByIdAsync(id);
            if (banner == null) return NotFound();

            var vm = new MenuBannerDetailVM
            {
                Title = banner.Title,
                Img = banner.Img
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _menuBannerService.DeleteAsync(id);
            return Ok();
        }

    }
}
