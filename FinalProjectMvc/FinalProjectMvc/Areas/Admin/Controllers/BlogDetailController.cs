using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.BlogDetailBanner;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogDetailBannerController : Controller
    {
        private readonly IBlogDetailBannerService _blogDetailBannerService;

        public BlogDetailBannerController(IBlogDetailBannerService blogDetailBannerService)
        {
            _blogDetailBannerService = blogDetailBannerService;
        }



        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Index()
        {
            var vmList = await _blogDetailBannerService.GetAllAsync();
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
        public async Task<IActionResult> Create(BlogDetailBannerCreateVM model)
        {
            if (!ModelState.IsValid) return View(model);

            await _blogDetailBannerService.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }



        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Edit(int id)
        {
            var banner = await _blogDetailBannerService.GetByIdAsync(id);
            if (banner == null) return NotFound();

            var vm = new BlogDetailBannerEditVM
            {
                Id = banner.Id,
                Title = banner.Title,
                Img = banner.Img
            };

            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Edit(BlogDetailBannerEditVM model)
        {
            if (!ModelState.IsValid) return View(model);

            await _blogDetailBannerService.EditAsync(model);
            return RedirectToAction(nameof(Index));
        }



        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Detail(int id)
        {
            var banner = await _blogDetailBannerService.GetByIdAsync(id);
            if (banner == null) return NotFound();

            var vm = new BlogDetailBannerDetailVM
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
            await _blogDetailBannerService.DeleteAsync(id);
            return Ok();
        }

    }
}
