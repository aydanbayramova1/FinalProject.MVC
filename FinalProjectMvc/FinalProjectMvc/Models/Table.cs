using System.ComponentModel.DataAnnotations;
namespace FinalProjectMvc.Models
{
    public class Table : BaseEntity
    {
        [Required]
        public int Number { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public int Capacity { get; set; } 
        public ICollection<Reservation> Reservations { get; set; }
    }
}
