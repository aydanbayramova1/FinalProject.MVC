using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.ContactMessage;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.Controllers
{
    public class ContactMessageController : Controller
    {
        private readonly IContactMessageService _contactMessageService;
        private readonly IContactUsService _contactUsService;

        public ContactMessageController(
            IContactMessageService contactMessageService,
            IContactUsService contactUsService)
        {
            _contactMessageService = contactMessageService;
            _contactUsService = contactUsService;
        }

        [HttpPost]
        public async Task<IActionResult> Submit(ContactPageVM vm)
        {
            if (!ModelState.IsValid)
            {
                vm.ContactInfo = (await _contactUsService.GetAllAsync()).FirstOrDefault();
                return View("~/Views/Contact/Index.cshtml", vm);
            }

            await _contactMessageService.CreateAsync(vm.MessageForm);

            TempData["Success"] = "Your message has been sent successfully!";
            return RedirectToAction("Index", "Home");
        }
    }
}
