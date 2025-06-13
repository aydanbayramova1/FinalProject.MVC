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

    // Ana səhifə - ilk 3 bloqu göstərir
    public IActionResult Index()
    {
        var allBlogs = _blogService.GetAllBlogs(); // Bütün blogları gətir
        ViewBag.TotalBlogs = allBlogs.Count(); // Toplam blog sayısı
        return View(allBlogs);
    }

    // AJAX üçün - növbəti blogları yükləyir
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
