using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.Helpers
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;

        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null && file.Length > _maxFileSize)
            {
                return new ValidationResult($"Maksimum fayl ölçüsü {_maxFileSize / 1024 / 1024} MB-dır.");
            }

            return ValidationResult.Success;
        }
    }
}