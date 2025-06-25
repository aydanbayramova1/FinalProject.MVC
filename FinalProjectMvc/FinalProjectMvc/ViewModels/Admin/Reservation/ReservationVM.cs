using FinalProjectMvc.Helpers.Enums;
using System;

namespace FinalProjectMvc.ViewModels.Admin.Reservation
{
    public class ReservationVM
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan TimeFrom { get; set; }
        public TimeSpan TimeTo { get; set; }
        public int GuestCount { get; set; }
        public string TableInfo { get; set; }   
        public string TableLocation { get; set; }
        public ReservationStatus Status { get; set; }
        public bool IsConfirmed => Status == ReservationStatus.Approved;
        public bool IsRejected => Status == ReservationStatus.Rejected;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
