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

        public async Task<IActionResult> Index()
        {
            var vms = await _service.GetAllAsync();
            return View(vms);
        }

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

        public async Task<IActionResult> Edit()
        {
            var vm = await _service.GetEditAsync();
            if (vm == null) return NotFound();

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(OfferImageEditVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            await _service.EditAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail()
        {
            var vm = await _service.GetDetailAsync();
            if (vm == null) return NotFound();

            return View(vm);
        }

        public async Task<IActionResult> DetailById(int id)
        {
            var vm = await _service.GetImageDetailByIdAsync(id);
            if (vm == null) return NotFound();

            return View("Detail", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Delete()
        {
            await _service.DeleteAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}