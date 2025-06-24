using FinalProjectMvc.Helpers.Enums;
using FinalProjectMvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public async Task<IActionResult> Index()
        {
            var reservations = await _reservationService.GetAllReservationsAsync();
            return View(reservations);
        }

        public async Task<IActionResult> Details(int id)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            if (reservation == null)
            {
                TempData["Error"] = "Rezervasiya tapılmadı.";
                return RedirectToAction(nameof(Index));
            }
            return View(reservation);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int id, ReservationStatus status)
        {
            var result = await _reservationService.UpdateReservationStatusAsync(id, status);

            if (result)
            {
                TempData["Success"] = status == ReservationStatus.Confirmed ?
              "Rezervasiya təsdiqləndi və müştəriyə email göndərildi." :
            "Rezervasiya ləğv edildi və müştəriyə email göndərildi.";
            }
            else
            {
                TempData["Error"] = "Status yenilənmədi. Xəta baş verdi.";
            }

            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _reservationService.DeleteReservationAsync(id);

            if (result)
            {
                TempData["Success"] = "Rezervasiya silindi.";
            }
            else
            {
                TempData["Error"] = "Rezervasiya silinmədi. Xəta baş verdi.";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<JsonResult> CheckTableAvailability(int tableId, DateTime date, string timeFrom, string timeTo)
        {
            if (TimeSpan.TryParse(timeFrom, out var fromTime) && TimeSpan.TryParse(timeTo, out var toTime))
            {
                var isAvailable = await _reservationService.IsTableAvailableAsync(tableId, date, fromTime, toTime);

                if (!isAvailable)
                {
                    return Json(new
                    {
                        available = false,
                        message = $"Bu masa {date:dd.MM.yyyy} tarixində {fromTime:hh\\:mm} - {toTime:hh\\:mm} saatları arasında məşğuldur."
                    });
                }

                return Json(new { available = true });
            }

            return Json(new { available = false, message = "Yanlış saat formatı." });
        }
    }
}
