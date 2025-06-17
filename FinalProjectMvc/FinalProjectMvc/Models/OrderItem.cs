using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProjectMvc.Models
{
    public class OrderItem : BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int SizeId { get; set; }
        public Size Size { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(8,2)")]
        public decimal UnitPrice { get; set; }
    }
}
