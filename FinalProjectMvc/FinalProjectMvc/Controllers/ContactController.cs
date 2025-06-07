using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.ContactMessage;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactMessageService _contactMessageService;

        public ContactController(IContactMessageService contactMessageService)
        {
            _contactMessageService = contactMessageService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var vm = new ContactPageVM
            {
                MessageForm = new ContactMessageCreateVM(),
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(ContactPageVM model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            await _contactMessageService.CreateAsync(model.MessageForm);

            TempData["Success"] = "Mesajınız uğurla göndərildi!";
            return RedirectToAction("Index");
        }
    }
}