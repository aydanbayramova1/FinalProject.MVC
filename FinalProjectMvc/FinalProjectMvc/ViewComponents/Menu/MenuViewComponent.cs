using FinalProjectMvc.Models;
using FinalProjectMvc.Services;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.Pricing;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.ViewComponents.Menu
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly ISizeService _sizeService;
        public MenuViewComponent(ICategoryService categoryService, IProductService productService,ISizeService sizeService)
        {
            _categoryService = categoryService;
            _productService= productService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryService.GetAllAsync();
            var products = await _productService.GetAllAsync();

            var filteredCategories = categories
                .Where(c => products.Any(p => p.CategoryId == c.Id))
                .ToList();

            if (!filteredCategories.Any())
            {
                return Content(string.Empty);
            }

            var vm = new PricingVM
            {
                Categories = filteredCategories,
                Products = products
            };

            return View(vm);
        }
    }
}
