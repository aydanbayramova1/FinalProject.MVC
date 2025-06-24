using System.ComponentModel.DataAnnotations;
using FinalProjectMvc.Helpers.Enums;

namespace FinalProjectMvc.Models
{
    public class Reservation : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Fullname { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public TimeSpan TimeFrom { get; set; }
        [Required]
        public TimeSpan TimeTo { get; set; }
        [Required]
        [Range(1, 100)]
        public int GuestCount { get; set; }
        public int TableId { get; set; }
        public Table Table { get; set; }
        public ReservationStatus Status { get; set; } = ReservationStatus.Pending;
    }
}
