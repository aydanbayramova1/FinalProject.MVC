namespace FinalProjectMvc.ViewModels.Admin.OrderItem
{
    public class OrderItemDetailVM
    {
        public string ProductName { get; set; }
        public string SizeName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => UnitPrice * Quantity;
    }
}
