namespace Entities
{
    public class OrderItem
    {
        public int IdOrderItem { get; set; }
        public int IdOrder { get; set; }
        public int IdProduct { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
