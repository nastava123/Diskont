namespace WebApi.Dto
{
    // Šta se dodaje u korpu. Cena se namerno ne šalje: uzima se iz baze.
    public class AddToCartRequest
    {
        public int IdProduct { get; set; }
        public int Quantity { get; set; }
    }
}
