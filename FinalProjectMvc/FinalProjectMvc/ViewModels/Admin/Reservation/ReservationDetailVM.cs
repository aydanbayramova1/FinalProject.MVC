using FinalProjectMvc.Helpers.Enums;
using FinalProjectMvc.Models;
using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.Reservation
{
    public class ReservationDetailVM
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan TimeFrom { get; set; }
        public TimeSpan TimeTo { get; set; }
        public int GuestCount { get; set; }
        public int TableId { get; set; }
        public string TableNumber { get; set; }
        public int TableCapacity { get; set; }
        public string TableLocation { get; set; }
        public ReservationStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
