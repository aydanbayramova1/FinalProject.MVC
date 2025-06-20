using FinalProjectMvc.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMvc.Controllers
{
    public class PaymentController : Controller
    {
        private readonly AppDbContext _context;

        public PaymentController(AppDbContext context)
        {
            _context = context;
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
            var reservation = await _context.Reservations.Include(r => r.Order).FirstOrDefaultAsync(r => r.Id == reservationId);
            if (reservation == null) return NotFound();

            reservation.IsPaid = true;
            reservation.Order.IsPaid = true;

            await _context.SaveChangesAsync();

            return RedirectToAction("Success");
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}