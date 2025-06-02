using System.ComponentModel.DataAnnotations;
using Microsoft.Build.Framework;

namespace FinalProjectMvc.ViewModels.Admin.Scrolling
{
    public class ScrollingEditVM
    {
        public int Id { get; set; }
        //[Required(ErrorMessage = "Title is required.")]
        //[RegularExpression("^[A-Za-z ]+$", ErrorMessage = "Title must contain only letters and spaces, no digits or symbols.")]
        public string Name { get; set; }

        public string? Img { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
