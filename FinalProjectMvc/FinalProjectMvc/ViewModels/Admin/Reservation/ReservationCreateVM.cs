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
        [Required(ErrorMessage = "Full name is required.")]
        [RegularExpression(@"^[A-Za-zƏəÖöÜüÇçŞşĞğIıİi\s]{1,100}$",
              ErrorMessage = "Only letters and spaces are allowed, no numbers or symbols.")]
        [StringLength(100)]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            ErrorMessage = "Only @ and . are allowed in email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^[0-9+\-]{7,20}$",
            ErrorMessage = "Phone can contain only digits, + or - characters.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }= DateTime.Now;

        [Required(ErrorMessage = "Start time is required.")]
        [DataType(DataType.Time)]
        public TimeSpan TimeFrom { get; set; }

        [Required(ErrorMessage = "End time is required.")]
        [DataType(DataType.Time)]
        public TimeSpan TimeTo { get; set; }

        [Required(ErrorMessage = "Number of guests is required.")]
        [Range(1, 15, ErrorMessage = "Guests must be between 1 and 15.")]
        public int Guests { get; set; }

        [Required(ErrorMessage = "Please select a table.")]
        public int TableId { get; set; }

        public List<OpeningHourVM> OpeningHours { get; set; } = new();
        public List<TableVM> Tables { get; set; } = new();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var isWeekend = Date.DayOfWeek == DayOfWeek.Saturday || Date.DayOfWeek == DayOfWeek.Sunday;
            var minTime = new TimeSpan(9, 0, 0);
            var maxTime = isWeekend ? new TimeSpan(22, 30, 0) : new TimeSpan(21, 0, 0);

            if (TimeFrom < minTime || TimeFrom > maxTime)
            {
                yield return new ValidationResult(
                    $"Start time must be between {minTime:hh\\:mm} and {maxTime:hh\\:mm} on {(isWeekend ? "weekends" : "weekdays")}.",
                    new[] { nameof(TimeFrom) });
            }

            if (TimeTo < minTime || TimeTo > maxTime)
            {
                yield return new ValidationResult(
                    $"End time must be between {minTime:hh\\:mm} and {maxTime:hh\\:mm} on {(isWeekend ? "weekends" : "weekdays")}.",
                    new[] { nameof(TimeTo) });
            }

            if (TimeFrom >= TimeTo)
            {
                yield return new ValidationResult("End time must be later than start time.", new[] { nameof(TimeTo) });
            }
        }
    }
}

