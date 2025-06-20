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
            var categories = await _reservationService.GetAllCategoriesAsync();

            var categoryVMs = categories.Select(c => new CategoryVM
            {
                Id = c.Id,
                Name = c.Name,
                Image = c.Image,
                CategoryTypeId = c.CategoryTypeId
            }).ToList();

            var vm = new ReservationCreateVM
            {
                Tables = await _reservationService.GetAllTablesAsync(),
                Products = await _reservationService.GetMenuProductsAsync(),
                Categories = categoryVMs
            };

            return View(vm);
        }
    }
}
