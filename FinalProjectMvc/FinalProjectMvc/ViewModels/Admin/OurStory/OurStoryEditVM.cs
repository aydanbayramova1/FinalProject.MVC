﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using FinalProjectMvc.Helpers;
using Microsoft.CodeAnalysis.Differencing;

namespace FinalProjectMvc.ViewModels.Admin.OurStory
{
    public class OurStoryEditVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Section title is required.")]
        [MaxLength(60, ErrorMessage = "Section title cannot exceed 60 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Section title cannot contain numbers or special characters.")]
        public string SectionTitle { get; set; }

        [Required(ErrorMessage = "Section subtitle is required.")]
        [MaxLength(70, ErrorMessage = "Section subtitle cannot exceed 70 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Section subtitle cannot contain numbers or special characters.")]
        public string SectionSubtitle { get; set; }

        [Required(ErrorMessage = "Section description is required.")]
        [MaxLength(160, ErrorMessage = "Section description cannot exceed 160 characters.")]
        public string SectionDescription { get; set; }

        public string? Image { get; set; }

        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = "Image file size cannot exceed 3MB.")]
        [AllowedExtensions(new[] { ".jpg", ".jpeg", ".png", ".webp" }, ErrorMessage = "Only image files (.jpg, .jpeg, .png, .webp) are allowed.")]
        public IFormFile? ImageFile { get; set; }
    }

}
