using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.ContactBanner;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactBannerController : Controller
    {
        private readonly IContactBannerService _contactBannerService;

        public ContactBannerController(IContactBannerService contactBannerService)
        {
            _contactBannerService = contactBannerService;
        }

        public async Task<IActionResult> Index()
        {
            var banners = await _contactBannerService.GetAllAsync();
            var vmList = banners.Select(b => new ContactBannerVM
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
        public async Task<IActionResult> Create(ContactBannerCreateVM model)
        {
            if (!ModelState.IsValid) return View(model);

            await _contactBannerService.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var banner = await _contactBannerService.GetByIdAsync(id);
            if (banner == null)
                throw new KeyNotFoundException($"ContactBanner with ID {id} not found.");

            var vm = new ContactBannerEditVM
            {
                Id = banner.Id,
                Title = banner.Title,
                Img = banner.Img
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ContactBannerEditVM model)
        {
            if (!ModelState.IsValid) return View(model);

            await _contactBannerService.EditAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int id)
        {
            var banner = await _contactBannerService.GetByIdAsync(id);
            if (banner == null)
                throw new KeyNotFoundException($"ContactBanner with ID {id} not found.");

            var vm = new ContactBannerDetailVM
            {
                Title = banner.Title,
                Img = banner.Img
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _contactBannerService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
