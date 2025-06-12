using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.Pricing;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.ViewComponents.Home
{
    public class PricingViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public PricingViewComponent(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
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
