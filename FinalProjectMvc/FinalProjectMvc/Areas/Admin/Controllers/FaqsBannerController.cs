using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.FaqsBanner;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FaqsBannerController : Controller
    {
        private readonly IFaqsBannerService _faqsBannerService;

        public FaqsBannerController(IFaqsBannerService faqsBannerService)
        {
            _faqsBannerService = faqsBannerService;
        }



        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Index()
        {
            var vmList = await _faqsBannerService.GetAllAsync();
            ViewBag.CanCreate = vmList.Count == 0;
            return View(vmList);
        }


        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(FaqsBannerCreateVM model)
        {
            if (!ModelState.IsValid) return View(model);

            await _faqsBannerService.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }



        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Edit(int id)
        {
            var banner = await _faqsBannerService.GetByIdAsync(id);
            if (banner == null) return NotFound();

            var vm = new FaqsBannerEditVM
            {
                Id = banner.Id,
                Title = banner.Title,
                Img = banner.Img
            };

            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]

        public async Task<IActionResult> Edit(FaqsBannerEditVM model)
        {
            if (!ModelState.IsValid) return View(model);

            await _faqsBannerService.EditAsync(model);
            return RedirectToAction(nameof(Index));
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Detail(int id)
        {
            var banner = await _faqsBannerService.GetByIdAsync(id);
            if (banner == null) return NotFound();

            var vm = new FaqsBannerDetailVM
            {
                Title = banner.Title,
                Img = banner.Img
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _faqsBannerService.DeleteAsync(id);
            return Ok();
        }

    }
}
