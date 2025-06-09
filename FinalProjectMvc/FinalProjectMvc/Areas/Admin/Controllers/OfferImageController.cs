using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.OfferImage;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OfferImageController : Controller
    {
        private readonly IOfferImageService _service;

        public OfferImageController(IOfferImageService service)
        {
            _service = service;
        }

        // Show list of OfferImages (usually 1)
        public async Task<IActionResult> Index()
        {
            var vms = await _service.GetAllAsync();
            return View(vms);
        }

        // GET: Create OfferImage
        public async Task<IActionResult> Create()
        {
            var offer = await _service.GetOurOfferAsync();
            if (offer == null) return NotFound();

            var vm = new OfferImageCreateVM
            {
                OurOfferId = offer.Id
            };

            return View(vm);
        }

        // POST: Create OfferImage
        [HttpPost]
        public async Task<IActionResult> Create(OfferImageCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                var offer = await _service.GetOurOfferAsync();
                if (offer != null) vm.OurOfferId = offer.Id;

                return View(vm);
            }

            await _service.CreateAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        // GET: Edit (Assumes only one OfferImage)
        public async Task<IActionResult> Edit()
        {
            var vm = await _service.GetEditAsync();
            if (vm == null) return NotFound();

            return View(vm);
        }

        // POST: Edit
        [HttpPost]
        public async Task<IActionResult> Edit(OfferImageEditVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            await _service.EditAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        // GET: Detail (single instance)
        public async Task<IActionResult> Detail()
        {
            var vm = await _service.GetDetailAsync();
            if (vm == null) return NotFound();

            return View(vm);
        }

        // GET: Detail by ID (optional if multiple images)
        public async Task<IActionResult> DetailById(int id)
        {
            var vm = await _service.GetImageDetailByIdAsync(id);
            if (vm == null) return NotFound();

            return View("Detail", vm);
        }

        // POST: Delete OfferImage (only one assumed)
        [HttpPost]
        public async Task<IActionResult> Delete()
        {
            await _service.DeleteAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}