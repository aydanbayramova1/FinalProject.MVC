using FinalProjectMvc.Models;
using FinalProjectMvc.Services;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.Menu;
using FinalProjectMvc.ViewModels.Admin.Pricing;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.ViewComponents.Menu
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        private readonly IMenuProductService _menuProductService;
        private readonly ISizeService _sizeService;
        public MenuViewComponent(ICategoryService categoryService, IMenuProductService menuProductService,ISizeService sizeService)
        {
            _categoryService = categoryService;
            _menuProductService = menuProductService;
            _sizeService = sizeService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryService.GetAllAsync();
            var products = await _menuProductService.GetAllAsync();
            var sizeS = await _sizeService.GetAllAsync();    

            var filteredCategories = categories
                .Where(c => products.Any(p => p.CategoryId == c.Id))
                .ToList();

            if (!filteredCategories.Any())
            {
                return Content(string.Empty);
            }

            var vm = new MenuVM
            {
                Categories = filteredCategories,
                MenuProductVMs = products,
                SizeVMs = sizeS,
            };

            return View(vm);
        }
    }
}
