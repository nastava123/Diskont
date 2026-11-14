using DataLayer.Interface;
using Entities;

namespace BusinessLayer
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;

        public OrderService(IOrderRepository orderRepository,
                            IOrderItemRepository orderItemRepository,
                            IProductRepository productRepository,
                            IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
        }

        public List<Order> GetAll()
        {
            return _orderRepository.GetAll();
        }

        public Order? Get(int id)
        {
            return _orderRepository.Get(id);
        }

        public List<OrderItem> GetItems(int idOrder)
        {
            return _orderItemRepository.GetByOrder(idOrder);
        }

        // ================== KORPA: radi se na Vežbama 5 ==================

        // Korak 1: vraća korpu korisnika (otvorenu porudžbinu) ili null
        public Order? GetCart(int idUser)
        {
            return _orderRepository.GetOrderByUser(idUser);
        }

        // Korak 2: stavke korpe
        public List<OrderItem> GetCartItems(int idUser)
        {
            Order? cart = _orderRepository.GetOrderByUser(idUser);
            if (cart == null)
                return new List<OrderItem>();

            return _orderItemRepository.GetByOrder(cart.IdOrder);
        }

        // Korak 3: dodavanje proizvoda u korpu
        public Order AddToCart(int idUser, int idProduct, int quantity)
        {
            // 3a. provere
            if (quantity <= 0)
                throw new ArgumentException("Količina mora biti veća od nule");

            User? user = _userRepository.Get(idUser);
            if (user == null)
                throw new InvalidOperationException("Korisnik ne postoji");
            if (!user.Status)
                throw new InvalidOperationException("Nalog je blokiran");

            Product? product = _productRepository.Get(idProduct);
            if (product == null)
                throw new InvalidOperationException("Proizvod ne postoji");
            if (product.Count < quantity)
                throw new InvalidOperationException(
                    $"Nema dovoljno na stanju (dostupno: {product.Count})");

            // 3b. korpa: postojeća ili nova
            Order? cart = _orderRepository.GetOrderByUser(idUser);
            if (cart == null)
            {
                cart = new Order();
                cart.IdUser = idUser;
                cart.TotalPrice = 0;
                cart.IsCompleted = false;
                _orderRepository.Add(cart);

                // ponovo čitamo iz baze da bismo dobili IdOrder i OrderDate
                cart = _orderRepository.GetOrderByUser(idUser)!;
            }

            // 3c. stavka: nova ili uvećanje postojeće
            OrderItem? item = _orderItemRepository.GetByOrderAndProduct(cart.IdOrder, idProduct);
            if (item == null)
            {
                item = new OrderItem();
                item.IdOrder = cart.IdOrder;
                item.IdProduct = idProduct;
                item.Quantity = quantity;
                item.UnitPrice = product.Price;     // cena se uzima iz baze
                _orderItemRepository.Add(item);
            }
            else
            {
                item.Quantity += quantity;
                _orderItemRepository.Update(item);
            }

            // 3d. rezervacija robe i novi iznos korpe
            product.Count -= quantity;
            _productRepository.Update(product);

            UpdateTotal(cart);
            return cart;
        }

        // Korak 4: uklanjanje proizvoda iz korpe
        public bool RemoveFromCart(int idUser, int idProduct)
        {
            Order? cart = _orderRepository.GetOrderByUser(idUser);
            if (cart == null)
                throw new InvalidOperationException("Korpa je prazna");

            OrderItem? item = _orderItemRepository.GetByOrderAndProduct(cart.IdOrder, idProduct);
            if (item == null)
                throw new InvalidOperationException("Proizvod nije u korpi");

            // roba se vraća na stanje
            Product? product = _productRepository.Get(idProduct);
            if (product != null)
            {
                product.Count += item.Quantity;
                _productRepository.Update(product);
            }

            _orderItemRepository.Delete(item);
            UpdateTotal(cart);
            return true;
        }

        // Korak 5: završetak kupovine
        public Order Checkout(int idUser)
        {
            Order? cart = _orderRepository.GetOrderByUser(idUser);
            if (cart == null || _orderItemRepository.GetByOrder(cart.IdOrder).Count == 0)
                throw new InvalidOperationException("Korpa je prazna");

            cart.IsCompleted = true;
            cart.OrderDate = DateTime.Now;      // datum porudžbine = trenutak kupovine
            _orderRepository.Update(cart);
            return cart;
        }

        // Korak 6: istorija završenih porudžbina
        public List<Order> GetHistory(int idUser)
        {
            return _orderRepository.GetAllByUser(idUser)
                                   .Where(o => o.IsCompleted)
                                   .ToList();
        }

       
        private void UpdateTotal(Order cart)
        {
            List<OrderItem> items = _orderItemRepository.GetByOrder(cart.IdOrder);
            cart.TotalPrice = items.Sum(i => i.Quantity * i.UnitPrice);
            _orderRepository.Update(cart);
        }
    }
}
