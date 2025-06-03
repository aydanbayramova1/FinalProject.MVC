using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.BlogBanner;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogBannerController : Controller
    {
        private readonly IBlogBannerService _blogBannerService;

        public BlogBannerController(IBlogBannerService blogBannerService)
        {
            _blogBannerService = blogBannerService;
        }

        public async Task<IActionResult> Index()
        {
            var vmList = await _blogBannerService.GetAllAsync();
            ViewBag.CanCreate = vmList.Count == 0;
            return View(vmList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BlogBannerCreateVM model)
        {
            if (!ModelState.IsValid) return View(model);

            await _blogBannerService.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var banner = await _blogBannerService.GetByIdAsync(id);
            if (banner == null) return NotFound();

            var vm = new BlogBannerEditVM
            {
                Id = banner.Id,
                Title = banner.Title,
                Img = banner.Img
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BlogBannerEditVM model)
        {
            if (!ModelState.IsValid) return View(model);

            await _blogBannerService.EditAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int id)
        {
            var banner = await _blogBannerService.GetByIdAsync(id);
            if (banner == null) return NotFound();

            var vm = new BlogBannerDetailVM
            {
                Title = banner.Title,
                Img = banner.Img
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _blogBannerService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
