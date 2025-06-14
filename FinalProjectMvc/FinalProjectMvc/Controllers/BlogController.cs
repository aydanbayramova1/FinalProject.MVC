using FinalProjectMvc.Data;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.Blog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class BlogController : Controller
{
    private readonly IBlogService _blogService;

    public BlogController(IBlogService blogService)
    {
        _blogService = blogService;
    }

    public IActionResult Index()
    {
        var allBlogs = _blogService.GetAllBlogs(); 
        ViewBag.TotalBlogs = allBlogs.Count();
        return View(allBlogs);
    }

    [HttpPost]
    public IActionResult LoadMoreBlogs(int skip)
    {
        var allBlogs = _blogService.GetAllBlogs();
        var nextBlogs = allBlogs.Skip(skip).Take(3).ToList();

        var hasMore = allBlogs.Count() > (skip + 3);

        return Json(new
        {
            success = true,
            blogs = nextBlogs,
            hasMore = hasMore
        });
    }

}
