using FinalProjectMvc.Models;
using FinalProjectMvc.ViewModels.Admin.Category;
using FinalProjectMvc.ViewModels.Admin.OfferItem;
using FinalProjectMvc.ViewModels.Admin.OpeningHour;
using FinalProjectMvc.ViewModels.Admin.OrderItem;
using FinalProjectMvc.ViewModels.Admin.Product;
using FinalProjectMvc.ViewModels.Admin.ProductSize;
using FinalProjectMvc.ViewModels.Admin.Table;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.Reservation
{
    public class ReservationCreateVM
    {
        [Required]
        [StringLength(100)]
        public string Fullname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan TimeFrom { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan TimeTo { get; set; }

        [Required]
        [Range(1, 100)]
        public int Guests { get; set; }

        [Required(ErrorMessage = "Please select a table.")]
        public int TableId { get; set; }
        public List<OpeningHourVM> OpeningHours { get; set; } = [];
        public List<TableVM> Tables { get; set; } = [];

    }
}
