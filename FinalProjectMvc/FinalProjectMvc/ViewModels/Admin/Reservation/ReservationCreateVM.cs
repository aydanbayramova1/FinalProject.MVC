using FinalProjectMvc.Models;
using FinalProjectMvc.ViewModels.Admin.Category;
using FinalProjectMvc.ViewModels.Admin.OfferItem;
using FinalProjectMvc.ViewModels.Admin.OrderItem;
using FinalProjectMvc.ViewModels.Admin.Product;
using FinalProjectMvc.ViewModels.Admin.ProductSize;
using FinalProjectMvc.ViewModels.Admin.Table;
using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.Reservation
{
    public class ReservationCreateVM
    {
        [Required]
        public string FullName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public TimeSpan Time { get; set; }

        [Required]
        [Range(1, 15)]
        public int GuestCount { get; set; }

        [Required]
        public int TableId { get; set; }

        public string Notes { get; set; }
        public List<TableVM> Tables { get; set; }
        public List<ProductWithSizeVM> Products { get; set; }
        public List<CategoryVM> Categories { get; set; }
        public List<OrderItemVM> CartItems { get; set; } = new();
    }
}
