using System.ComponentModel.DataAnnotations;

namespace FinalProjectMvc.ViewModels.Admin.Table
{
    public class TableCreateVM
    {
        [Required(ErrorMessage = "Masanın adı boş ola bilməz")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Tutum boş ola bilməz")]
        [Range(1, 15, ErrorMessage = "Tutum 1 ilə 15 arasında olmalıdır")]
        public int Capacity { get; set; }
    }
}
