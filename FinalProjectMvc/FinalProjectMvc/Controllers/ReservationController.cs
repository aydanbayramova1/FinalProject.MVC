using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.Reservation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Create()
        {
            var products = await _reservationService.GetMenuProductsAsync();
            var tables = await _reservationService.GetAllTablesAsync();

            var vm = new ReservationCreateVM
            {
                Products = products,
                Tables = tables
            };

            return View(vm);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] ReservationCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Products = await _reservationService.GetMenuProductsAsync();
                vm.Tables = await _reservationService.GetAllTablesAsync();
                return View(vm);
            }

            var success = await _reservationService.CreateAsync(vm);
            if (!success)
            {
                ModelState.AddModelError("", "Seçilmiş tarix və saat üçün uyğun boş masa yoxdur.");
                vm.Products = await _reservationService.GetMenuProductsAsync();
                vm.Tables = await _reservationService.GetAllTablesAsync();
                return View(vm);
            }

            return Ok();
        }

        [AllowAnonymous]
        public IActionResult Success()
        {
            return View();
        }
    }
}
