using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.ContactBanner;
using Microsoft.AspNetCore.Mvc;

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
            var vmList = await _contactBannerService.GetAllAsync();
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
            if (banner == null) return NotFound();

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
            if (banner == null) return NotFound();

            var vm = new ContactBannerVM
            {
                Id = banner.Id,
                Title = banner.Title,
                Img = banner.Img
            };

            return View(vm); 
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _contactBannerService.DeleteAsync(id);
            return Ok();
        }

    }
}
