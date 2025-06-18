using System.ComponentModel.DataAnnotations;
using FinalProjectMvc.ViewModels.Admin.Reservation;

namespace FinalProjectMvc.ViewModels.Admin.Table
{
    public class TableVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Ad boş ola bilməz")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Tutum boş ola bilməz")]
        [Range(1, 15, ErrorMessage = "Tutum 1 ilə 15 arasında olmalıdır")]
        public int Capacity { get; set; }
    }
}
