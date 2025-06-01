using FinalProjectMvc.Services;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.AboutBanner;
using Microsoft.AspNetCore.Mvc;
using System;

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
            var banners = await _aboutBannerService.GetAllAsync();
            var vmList = banners.Select(b => new AboutBannerVM
            {
                Id = b.Id,
                Title = b.Title,
                Img = b.Img
            }).ToList();

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
            if (banner == null)
                throw new KeyNotFoundException($"AboutBanner with ID {id} not found.");

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
            if (banner == null)
                throw new KeyNotFoundException($"AboutBanner with ID {id} not found.");

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
            return RedirectToAction(nameof(Index));
        }
    }
}
