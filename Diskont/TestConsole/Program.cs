using BusinessLayer;
using DataLayer.Implementations;
using Entities;

Console.OutputEncoding = System.Text.Encoding.UTF8;

// Servisi dobijaju repozitorijume kroz konstruktor
ProductService productService = new ProductService(new ProductRepository());
CategoryService categoryService = new CategoryService(new CategoryRepository());
UserService userService = new UserService(new UserRepository());

Console.WriteLine("=== Kategorije");
foreach (Category c in categoryService.GetAll())
    Console.WriteLine($"{c.IdCategory}. {c.Name}");

Console.WriteLine();
Console.WriteLine("=== Proizvodi na stanju");
foreach (Product p in productService.GetAvailable())
    Console.WriteLine($"{p.IdProduct}. {p.Name,-15} {p.Price,8:N2} din  ({p.Count} kom)");

Console.WriteLine();
Console.WriteLine("=== Neispravan proizvod");
try
{
    Product product = new Product();
    product.Name = "";
    product.Price = 100;
    product.IdCategory = 1;
    productService.Add(product);
}
catch (ArgumentException ex)
{
    Console.WriteLine("Greška: " + ex.Message);
}

Console.WriteLine();
Console.WriteLine("=== Proizvod koji ne postoji");
Product? missing = productService.Get(9999);
Console.WriteLine(missing == null ? "Proizvod 9999 ne postoji" : missing.Name);

Console.WriteLine();
Console.WriteLine("=== Korisnici");
foreach (User u in userService.GetAll())
    Console.WriteLine($"{u.IdUser}. {u.FirstName} {u.LastName} <{u.Email}>  " +
                      (u.Status ? "aktivan" : "blokiran"));
