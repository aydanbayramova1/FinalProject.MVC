
using FinalProjectMvc.Services;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.ContactMessage;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactMessageService _contactMessageService;
        private readonly ILogger<ContactController> _logger;

        public ContactController(IContactMessageService contactMessageService, ILogger<ContactController> logger)
        {
            _contactMessageService = contactMessageService;
            _logger = logger;
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
        public async Task<IActionResult> Submit(ContactPageVM model)
        {
            _logger.LogInformation("Contact form submitted with email: {Email}", model.MessageForm.Email);

            if (string.IsNullOrWhiteSpace(model.MessageForm.Email))
            {
                ModelState.AddModelError("MessageForm.Email", "Email is required.");
                return View("Index", model);
            }

            if (!IsValidEmail(model.MessageForm.Email))
            {
                ModelState.AddModelError("MessageForm.Email", "Please enter a valid email address.");
                return View("Index", model);
            }

            try
            {
                await _contactMessageService.CreateAsync(model.MessageForm);
                _logger.LogInformation("Contact message created successfully");
                TempData["SuccessMessage"] = "Your message has been sent successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving contact message");
                ModelState.AddModelError("", "An error occurred while sending your message. Please try again.");
                return View("Index", model);
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

    }
}