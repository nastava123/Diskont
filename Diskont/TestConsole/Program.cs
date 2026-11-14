using BusinessLayer;
using DataLayer.Implementations;
using Entities;

Console.OutputEncoding = System.Text.Encoding.UTF8;

ProductService productService = new ProductService(new ProductRepository());
OrderService orderService = new OrderService(new OrderRepository(),
                                             new OrderItemRepository(),
                                             new ProductRepository(),
                                             new UserRepository());
int idUser = 1;     // Petar Petrović iz 02_podaci.sql

// ---- pomoćna metoda: ispis korpe ----
void PrintCart()
{
    Order? cart = orderService.GetCart(idUser);
    if (cart == null)
    {
        Console.WriteLine("   Korpa je prazna.");
        return;
    }
    foreach (OrderItem item in orderService.GetCartItems(idUser))
    {
        Product? p = productService.Get(item.IdProduct);
        Console.WriteLine($"   {item.Quantity} x {p?.Name,-15} {item.Quantity * item.UnitPrice,8:N2} din");
    }
    Console.WriteLine($"   UKUPNO: {cart.TotalPrice:N2} din");
}

// ---- pomoćna metoda: poziv uz hvatanje grešaka ----
void Try(string title, Action action)
{
    Console.WriteLine();
    Console.WriteLine("=== " + title);
    try
    {
        action();
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine("   Neispravan unos: " + ex.Message);
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine("   Nije dozvoljeno: " + ex.Message);
    }
}

Try("Dodavanje: 2 x Hleb", () => { orderService.AddToCart(idUser, 3, 2); PrintCart(); });
Try("Dodavanje: 1 x Kafa", () => { orderService.AddToCart(idUser, 4, 1); PrintCart(); });
Try("Dodavanje: još 1 x Hleb", () => { orderService.AddToCart(idUser, 3, 1); PrintCart(); });
Try("Stanje hleba posle rezervacije", () =>
    Console.WriteLine($"   Hleb na stanju: {productService.Get(3)!.Count} kom"));
Try("Količina 0", () => orderService.AddToCart(idUser, 5, 0));
Try("Previše komada", () => orderService.AddToCart(idUser, 4, 500));
Try("Proizvod koji ne postoji", () => orderService.AddToCart(idUser, 9999, 1));
Try("Uklanjanje kafe", () => { orderService.RemoveFromCart(idUser, 4); PrintCart(); });
Try("Završetak kupovine", () =>
{
    Order order = orderService.Checkout(idUser);
    Console.WriteLine($"   Porudžbina {order.IdOrder} je završena, iznos {order.TotalPrice:N2} din");
    PrintCart();
});
Try("Završetak kupovine sa praznom korpom", () => orderService.Checkout(idUser));
Try("Istorija porudžbina", () =>
{
    foreach (Order o in orderService.GetHistory(idUser))
        Console.WriteLine($"   #{o.IdOrder}  {o.OrderDate:dd.MM.yyyy HH:mm}  {o.TotalPrice,8:N2} din");
});
