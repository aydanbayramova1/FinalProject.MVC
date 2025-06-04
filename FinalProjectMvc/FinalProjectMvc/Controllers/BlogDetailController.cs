using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.Blog;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.Controllers
{
    public class BlogDetailController : Controller
    {
        private readonly IBlogService _blogService;
        public BlogDetailController(IBlogService blog)
        {
            _blogService = blog;
        }
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null) return BadRequest();
            BlogDetailVM blog = await _blogService.GetByIdAsync((int)id);
            if (blog == null) return NotFound();
            return View(blog);
        }
    }
}
