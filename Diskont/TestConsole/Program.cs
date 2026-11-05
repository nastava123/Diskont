// See https://aka.ms/new-console-template for more information
using BusinessLayer;
using DataLayer.Implementations;
using Entities;

Console.WriteLine("Hello, World!");


ProductService productService = new ProductService(new ProductRepository());


try
{
    Product product = new Product();
    product.Name = "test";
    product.Price = 1000;
    product.Count = 2;
    product.IdCategory = 2;

    productService.Add(product);
}
catch (ArgumentException ex)
{
    Console.WriteLine(ex.Message);
}

Product? product1 = productService.Get(1004);
if (product1 != null)
{
    product1.Price = 80;
    productService.Update(product1);
    Console.WriteLine(product1.Price);
}
try
{
    if (productService.Delete(product1!))
    {
        Console.WriteLine("Uspesno");
    }
}catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}




