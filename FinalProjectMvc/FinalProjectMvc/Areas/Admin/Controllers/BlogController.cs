using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.Blog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Index()
        {
            var blogs = await _blogService.GetAllAsync();
            return View(blogs);
        }


        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(BlogCreateVM model)
        {
            if (!ModelState.IsValid) return View(model);

            await _blogService.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Edit(int id)
        {
            var blog = await _blogService.GetByIdAsync(id);
            if (blog == null) return NotFound();

            var editVM = new BlogEditVM
            {
                Title = blog.Title,
                Description = blog.Description,
                Img = blog.Img
            };

            return View(editVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Edit(int id, BlogEditVM model)
        {
            if (!ModelState.IsValid) return View(model);

            await _blogService.UpdateAsync(id, model);
            return RedirectToAction(nameof(Index));
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Detail(int id)
        {
            var blog = await _blogService.GetByIdAsync(id);
            if (blog == null) return NotFound();

            return View(blog); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _blogService.DeleteAsync(id);
            return Ok();
        }

    }
}
