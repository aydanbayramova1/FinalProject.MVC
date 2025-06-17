using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProjectMvc.Models
{
    public class Order : BaseEntity
    {
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }

        public ICollection<OrderItem> Items { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal TotalAmount { get; set; }

        public bool IsPaid { get; set; } = false;
    }
}
