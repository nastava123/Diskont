namespace Entities
{
    public class Order
    {
        public int IdOrder { get; set; }
        public int IdUser { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
