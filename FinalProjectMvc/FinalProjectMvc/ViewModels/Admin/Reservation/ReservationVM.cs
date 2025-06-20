using FinalProjectMvc.Models;
using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.Reservation
{
    public class ReservationVM
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan Time { get; set; }

        public int GuestCount { get; set; }

        public int TableId { get; set; }
        public string TableLocation { get; set; }
        public string TableNumber { get; set; } 

        public bool IsConfirmed { get; set; }

        public bool IsRejected { get; set; }

        public bool IsPaid { get; set; }

        public decimal? OrderTotal { get; set; } 
    }
}
