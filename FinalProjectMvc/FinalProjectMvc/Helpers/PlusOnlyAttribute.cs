using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.Helpers
{
    public class PlusOnlyAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var str = value as string;
            if (!string.IsNullOrEmpty(str))
            {
                if (str != "+")
                    return new ValidationResult("Suffix can only be the '+' character.");
            }
            return ValidationResult.Success;
        }
    }
}
