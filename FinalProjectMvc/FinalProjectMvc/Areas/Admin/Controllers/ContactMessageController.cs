using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.ContactMessage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactMessageController : Controller
    {
        private readonly IContactMessageService _contactMessageService;

        public ContactMessageController(IContactMessageService contactMessageService)
        {
            _contactMessageService = contactMessageService;
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Index()
        {
            var messages = await _contactMessageService.GetAllAsync();
            return View(messages);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Reply(int id)
        {
            var message = await _contactMessageService.GetByIdAsync(id);
            if (message == null)
                return NotFound();

            var vm = new ContactMessageReplyVM
            {
                Id = message.Id,
                FirstName = message.FirstName,
                LastName = message.LastName,
                Email = message.Email,
                Message = message.Message
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Reply(ContactMessageReplyVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            try
            {
                await _contactMessageService.ReplyToMessageAsync(vm);
                TempData["Success"] = "Reply sent successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["Error"] = "Failed to send reply. Please try again.";
                return View(vm);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _contactMessageService.DeleteAsync(id);
            return Ok();
        }
    }
}