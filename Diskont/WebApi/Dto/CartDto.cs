namespace WebApi.Dto
{
    // Korpa spremna za prikaz: stavke sa nazivima proizvoda i ukupan iznos
    public class CartDto
    {
        public int IdOrder { get; set; }
        public decimal TotalPrice { get; set; }
        public List<CartItemDto> Items { get; set; } = new List<CartItemDto>();
    }

    public class CartItemDto
    {
        public int IdProduct { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
    }
}
