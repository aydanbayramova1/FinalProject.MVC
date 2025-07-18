﻿using FinalProjectMvc.Data;
using FinalProjectMvc.Helpers.Enums;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.Reservation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly AppDbContext _context;

        public ReservationController(IReservationService reservationService, AppDbContext context)
        {
            _reservationService = reservationService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var reservations = await _reservationService.GetAllReservationsAsync();
            return View(reservations);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var vm = await _reservationService.GetReservationByIdAsync(id);

            if (vm == null)
            {
                TempData["Error"] = "Reservation not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(vm);
        }




        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int id, ReservationStatus status)
        {
            var result = await _reservationService.UpdateReservationStatusAsync(id, status);

            if (result)
            {
                TempData["Success"] = status == ReservationStatus.Confirmed ?
             "Reservation has been confirmed and an email has been sent to the customer." :
             "Reservation has been cancelled and an email has been sent to the customer.";
            }
            else
            {
                TempData["Error"] = "Status was not updated. An error occurred.";


            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _reservationService.DeleteReservationAsync(id);

            if (result)
            {
                TempData["Success"] = "Reservation deleted.";
            }
            else
            {
                TempData["Error"] = "The reservation was not deleted. An error occurred.";
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
                        message = $"This table is busy from {fromTime:hh\\:mm} to {toTime:hh\\:mm} on {date:dd.MM.yyyy}."
                    });
                }

                return Json(new { available = true });
            }

            return Json(new { available = false, message = "Invalid time format." });
        }
    }
}
