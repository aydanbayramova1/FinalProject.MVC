using FinalProjectMvc.Models;
using FinalProjectMvc.ViewModels.Admin.ContactMessage;
using FinalProjectMvc.ViewModels.Admin.Subscribe;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.ViewComponents.Contact
{
    public class SubscribeViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var vm = new ContactPageVM
            {
                Subscribe = new SubscribeVM()
            };

            return View(vm);
        }
    }
}
