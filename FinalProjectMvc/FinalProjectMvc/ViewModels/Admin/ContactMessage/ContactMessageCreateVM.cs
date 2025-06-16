using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.ContactMessage
{
    public class ContactMessageCreateVM
    {
        [Required(ErrorMessage = "First name is required.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "First name can contain only letters and spaces.")]
        [StringLength(50, ErrorMessage = "First name can't be longer than 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Last name can contain only letters and spaces.")]
        [StringLength(50, ErrorMessage = "Last name can't be longer than 50 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Email must be a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is required.")]
        [RegularExpression(@"^\+?[0-9]{7,15}$", ErrorMessage = "Phone can contain only digits and an optional leading +.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Message is required.")]
        [StringLength(1000, ErrorMessage = "Message can't be longer than 1000 characters.")]
        public string Message { get; set; }
    }
}
