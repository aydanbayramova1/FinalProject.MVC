using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.CategoryType;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryTypeController : Controller
    {
        private readonly ICategoryTypeService _categoryTypeService;

        public CategoryTypeController(ICategoryTypeService categoryTypeService)
        {
            _categoryTypeService = categoryTypeService;
        }

        public async Task<IActionResult> Index()
        {
            var types = await _categoryTypeService.GetAllAsync();
            return View(types);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryTypeCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            await _categoryTypeService.CreateAsync(vm);
            return RedirectToAction(nameof(Index));
        }
    }
}
