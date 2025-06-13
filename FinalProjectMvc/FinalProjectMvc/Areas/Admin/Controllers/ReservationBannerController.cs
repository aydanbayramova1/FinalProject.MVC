using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.ReservationBanner;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReservationBannerController : Controller
    {
        private readonly IReservationBannerService _reservationBannerService;

        public ReservationBannerController(IReservationBannerService reservationBannerService)
        {
            _reservationBannerService = reservationBannerService;
        }

        public async Task<IActionResult> Index()
        {
            var vmList = await _reservationBannerService.GetAllAsync();
            ViewBag.CanCreate = vmList.Count == 0;
            return View(vmList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReservationBannerCreateVM model)
        {
            if (!ModelState.IsValid) return View(model);

            await _reservationBannerService.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var banner = await _reservationBannerService.GetByIdAsync(id);
            if (banner == null) return NotFound();

            var vm = new ReservationBannerEditVM
            {
                Id = banner.Id,
                Title = banner.Title,
                Img = banner.Img
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ReservationBannerEditVM model)
        {
            if (!ModelState.IsValid) return View(model);

            await _reservationBannerService.EditAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int id)
        {
            var banner = await _reservationBannerService.GetByIdAsync(id);
            if (banner == null) return NotFound();

            var vm = new ReservationBannerDetailVM
            {
                Title = banner.Title,
                Img = banner.Img
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _reservationBannerService.DeleteAsync(id);
            return Ok();
        }

    }
}
