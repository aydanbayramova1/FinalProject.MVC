using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.FaqCategory;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.ViewComponents.Faqs
{
    public class FaqsViewComponent : ViewComponent
    {
        private readonly IFaqCategoryService _faqCategoryService;
        private readonly ISettingService _settingService;

        public FaqsViewComponent(IFaqCategoryService faqCategoryService, ISettingService settingService)
        {
            _faqCategoryService = faqCategoryService;
            _settingService = settingService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _faqCategoryService.GetAllWithItemsAsync();
            var setting = await _settingService.GetSettingAsync();

            var model = new FaqSectionVM
            {
                FaqCategories = categories,
                Setting = setting
            };

            return View(model);
        }
    }
}
