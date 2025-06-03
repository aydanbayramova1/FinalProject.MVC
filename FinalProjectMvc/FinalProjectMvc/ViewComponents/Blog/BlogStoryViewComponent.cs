using FinalProjectMvc.Data;
using FinalProjectMvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.ViewComponents.Blog
{
    public class BlogStoryViewComponent : ViewComponent
    {
        private readonly IBlogService _blogService;
        private readonly AppDbContext _context;

        public BlogStoryViewComponent(IBlogService blogService, AppDbContext context)
        {
            _blogService = blogService;
            _context = context;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var blogs = await _blogService.GetAllAsync();
            var totalCount = await _context.Blogs.CountAsync(); 

            ViewBag.TotalCount = totalCount;
            ViewBag.InitialCount = blogs.Count;

            if (blogs == null || !blogs.Any())
            {
                return Content("");
            }
            return View(blogs); 
        }
    }
}
