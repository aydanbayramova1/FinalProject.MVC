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

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public IActionResult Index()
        {
            return View();
        }
 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReservationCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Products = await _reservationService.GetMenuProductsAsync();
                vm.Tables = await _reservationService.GetAllTablesAsync();
                vm.Categories = await _reservationService.GetAllCategoriesAsync();
                vm.OpeningHours = await _reservationService.GetOpeningHoursAsync(); 
                return View(vm);
            }

            var success = await _reservationService.CreateAsync(vm);
            if (!success)
            {
                ModelState.AddModelError("", "Uyğun masa tapılmadı.");
                vm.Products = await _reservationService.GetMenuProductsAsync();
                vm.Tables = await _reservationService.GetAllTablesAsync();
                vm.Categories = await _reservationService.GetAllCategoriesAsync();
                vm.OpeningHours = await _reservationService.GetOpeningHoursAsync();
                return View(vm);
            }

            return RedirectToAction("Success");
        }





        public IActionResult Success()
        {
            return View();
        }
    }
}
