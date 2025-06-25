using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.Reservation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FinalProjectMvc.Controllers
{
    [AllowAnonymous]
    [Authorize]
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly ITableService _tableService;
        private readonly IOpeningHourService _openingHourService;

        public ReservationController(IReservationService reservationService, ITableService tableService, IOpeningHourService openingHourService)
        {
            _reservationService = reservationService;
            _tableService = tableService;
            _openingHourService = openingHourService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new ReservationCreateVM
            {
                //OpeningHours = await _openingHourService.GetAsync(),
                Tables = await _tableService.GetAllAsync()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> GetAvailableTables(DateTime date, string timeFrom, string timeTo, int guests)
        {
            if (TimeSpan.TryParse(timeFrom, out var fromTime) && TimeSpan.TryParse(timeTo, out var toTime))
            {
                var tables = await _reservationService.GetAvailableTablesAsync(date, fromTime, toTime, guests);
                return Json(tables.Select(t => new
                {
                    id = t.Id,
                    number = t.Number,
                    capacity = t.Capacity,
                    location = t.Location
                }));
            }
            return Json(new List<object>());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> CreateReservation(ReservationCreateVM model)
        {
            //if (!ModelState.IsValid)
            //{
            //    var errors = ModelState.Values.SelectMany(v => v.Errors)
            //                                  .Select(e => e.ErrorMessage)
            //                                  .ToList();
            //    return Json(new { success = false, message = "Validation failed", errors });
            //}

            var result = await _reservationService.CreateReservationAsync(model);

            if (result)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = "The table is not available or is busy." });
            }



        }
    }
}