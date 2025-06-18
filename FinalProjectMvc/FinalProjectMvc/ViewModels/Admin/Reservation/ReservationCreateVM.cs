using FinalProjectMvc.Models;
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
        public int GuestCount { get; set; }
        public int TableId { get; set; }
    }
}
