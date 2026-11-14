using BusinessLayer;
using DataLayer.Implementations;
using Entities;



ProductService productService = new ProductService(new ProductRepository());
OrderService orderService = new OrderService(new OrderRepository(),
                                             new OrderItemRepository(),
                                             new ProductRepository(),
                                             new UserRepository());
