using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.ContactMessage;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.ViewComponents.Contact
{
    public class ContactViewComponent : ViewComponent
    {
        private readonly IContactUsService _contactUsService;

        public ContactViewComponent(IContactUsService contactUsService)
        {
            _contactUsService = contactUsService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var allContacts = await _contactUsService.GetAllAsync();
            var firstContact = allContacts.FirstOrDefault();

            var vm = new ContactPageVM
            {
                ContactInfo = firstContact,
                MessageForm = new ContactMessageCreateVM()
            };

            return View(vm);
        }
    }
}
