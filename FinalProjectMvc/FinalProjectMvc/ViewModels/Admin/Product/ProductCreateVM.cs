using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProjectMvc.ViewModels.Admin.Product
{
    public class ProductCreateVM
    {
        [Required]
        public string Name { get; set; }

        public IFormFile? ImageFile { get; set; }

        [Required]
        public string Ingredients { get; set; }

        [Required]
        [Range(0.01, 9999)]
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public List<SelectListItem>? Categories { get; set; }

        public List<int>? SelectedSizeIds { get; set; } 

        public List<SelectListItem>? Sizes { get; set; }
        public Dictionary<int, string> CategoryTypesById { get; set; }
    }
}
