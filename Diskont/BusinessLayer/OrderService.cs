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

        // ================== KORPA: Vežbe 5 ==================

        // Korak 1: vraća korpu korisnika (otvorenu porudžbinu) ili null
        public Order? GetCart(int idUser)
        {
            throw new NotImplementedException("");
        }

        // Korak 2: stavke korpe
        public List<OrderItem> GetCartItems(int idUser)
        {
            throw new NotImplementedException("");
        }

        // Korak 3: dodavanje proizvoda u korpu
        public Order AddToCart(int idUser, int idProduct, int quantity)
        {
            throw new NotImplementedException("");
        }

        // Korak 4: uklanjanje proizvoda iz korpe
        public bool RemoveFromCart(int idUser, int idProduct)
        {
            throw new NotImplementedException("");
        }

        // Korak 5: završetak kupovine
        public Order Checkout(int idUser)
        {
            throw new NotImplementedException("");
        }

        // Korak 6: istorija završenih porudžbina
        public List<Order> GetHistory(int idUser)
        {
            throw new NotImplementedException("");
        }
    }
}
