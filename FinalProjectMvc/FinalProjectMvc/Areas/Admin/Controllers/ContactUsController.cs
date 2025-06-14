using FinalProjectMvc.Services;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.ContactUs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactUsController : Controller
    {
        private readonly IContactUsService _contactUsService;

        public ContactUsController(IContactUsService contactUsService)
        {
            _contactUsService = contactUsService;
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Index()
        {
            var contacts = await _contactUsService.GetAllAsync();
            ViewBag.HasContact = contacts.Any(); 
            return View(contacts);
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Detail(int id)
        {
            var detail = await _contactUsService.GetByIdAsync(id);
            return View(detail);
        }


        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(ContactUsCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            await _contactUsService.CreateAsync(vm);
            return RedirectToAction(nameof(Index));
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Edit(int id)
        {
            var detail = await _contactUsService.GetByIdAsync(id);
            var vm = new ContactUsEditVM
            {
                Id = id,
                PhoneNumber = detail.PhoneNumber,
                Email = detail.Email,
                Address = detail.Address,
                MessageTitle = detail.MessageTitle,
                MessageDescription = detail.MessageDescription
            };
            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Edit(ContactUsEditVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            await _contactUsService.EditAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _contactUsService.DeleteAsync(id);
            return Ok();
        }

    }
}
