using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.Reservation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.Controllers
{
    [Authorize]
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Create(ReservationCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var success = await _reservationService.CreateAsync(vm);
            if (!success)
            {
                ModelState.AddModelError("", "No available table for the selected time and guest count.");
                return View(vm);
            }

            return RedirectToAction(nameof(Success));
        }

        [AllowAnonymous]
        public IActionResult Success()
        {
            return View();
        }
    }
}

