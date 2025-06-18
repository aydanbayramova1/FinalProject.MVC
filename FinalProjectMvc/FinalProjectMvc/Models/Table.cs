using System.ComponentModel.DataAnnotations;
namespace FinalProjectMvc.Models
{
    public class Table : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Capacity { get; set; } 
        public ICollection<Reservation> Reservations { get; set; }
    }
}
