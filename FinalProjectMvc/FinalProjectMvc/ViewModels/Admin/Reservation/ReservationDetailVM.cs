using FinalProjectMvc.Models;
using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.Reservation
{
    public class ReservationDetailVM
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }

        public int GuestCount { get; set; }
        public string TableLocation { get; set; }
        public int TableNumber { get; set; }

        public bool IsConfirmed { get; set; }
        public bool IsRejected { get; set; }
        public bool IsCanceled { get; set; }
        public bool IsPaid { get; set; }

        public decimal TotalAmount { get; set; }
        public List<ReservationOrderItemVM> OrderItems { get; set; }
    }
}
