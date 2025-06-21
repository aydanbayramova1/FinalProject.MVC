using FinalProjectMvc.Data;
using FinalProjectMvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.Controllers
{
    public class PaymentController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IEmailService _emailService;

        public PaymentController(AppDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<IActionResult> Index(int reservationId)
        {
            var reservation = await _context.Reservations
                .Include(r => r.Order).ThenInclude(o => o.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(r => r.Id == reservationId);

            if (reservation == null || reservation.IsPaid)
                return NotFound();

            return View(reservation); 
        }

        [HttpPost]
        public async Task<IActionResult> Pay(int reservationId)
        {
            var reservation = await _context.Reservations
                .Include(r => r.Order)
                .Include(r => r.Table)
                .FirstOrDefaultAsync(r => r.Id == reservationId);

            if (reservation == null || reservation.IsPaid) return NotFound();

            reservation.IsPaid = true;
            reservation.Order.IsPaid = true;

            await _context.SaveChangesAsync();

            await _emailService.SendAsync(
                reservation.Email,
                "Ödəniş tamamlandı",
                $@"<p>Hörmətli {reservation.FullName},</p>
           <p>Ödənişiniz uğurla tamamlandı.</p>
           <p><b>Rezervasiya məlumatları:</b></p>
           <ul>
               <li>Tarix: {reservation.Date:dd MMMM yyyy}</li>
               <li>Saat: {reservation.Time:hh\\:mm}</li>
               <li>Masa №: {reservation.Table.Number} ({reservation.Table.Location})</li>
           </ul>
           <p>Sizi gözləyirik!</p>");

            return RedirectToAction("Success", new { reservationId = reservation.Id });
        }


        public async Task<IActionResult> Success(int reservationId)
        {
            var reservation = await _context.Reservations
                .Include(r => r.Table)
                .FirstOrDefaultAsync(r => r.Id == reservationId);

            if (reservation == null) return NotFound();

            return View(reservation);
        }

    }
}