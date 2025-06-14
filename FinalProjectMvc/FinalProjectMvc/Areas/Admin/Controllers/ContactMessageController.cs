using FinalProjectMvc.Services.Interfaces;
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
