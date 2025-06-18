using FinalProjectMvc.Models;
using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.Reservation
{
    public class ReservationDetail
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
        public bool IsConfirmed { get; set; } = false;
        public bool IsRejected { get; set; } = false;
        public bool IsCanceled { get; set; } = false;
        public bool IsPaid { get; set; } = false;
        public Order? Order { get; set; }
    }
}
