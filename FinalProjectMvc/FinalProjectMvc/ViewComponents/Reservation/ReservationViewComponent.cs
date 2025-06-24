using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.ViewModels.Admin.Category;
using FinalProjectMvc.ViewModels.Admin.Reservation;
using FinalProjectMvc.Models; 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMvc.ViewComponents.Reservation
{
    public class ReservationViewComponent : ViewComponent
    {
        private readonly IReservationService _reservationService;

        public ReservationViewComponent(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var vm = new ReservationCreateVM
            {
                Tables = await _reservationService.GetAllTablesAsync(),
                OpeningHours = await _reservationService.GetOpeningHoursAsync() 
            };

            return View(vm);
        }

    }
}
