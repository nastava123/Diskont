using BusinessLayer;
using Entities;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/users/{idUser}/cart")]          // korpa pripada korisniku
    public class CartController : ControllerBase
    {
        private readonly OrderService _orderService;
        private readonly ProductService _productService;

        public CartController(OrderService orderService, ProductService productService)
        {
            _orderService = orderService;
            _productService = productService;
        }

        [HttpGet]                               // GET /api/users/1/cart
        public ActionResult<CartDto> Get(int idUser)
        {
            CartDto dto = new CartDto();
            Order? cart = _orderService.GetCart(idUser);
            if (cart == null)
                return dto;                     // prazna korpa nije greška

            dto.IdOrder = cart.IdOrder;
            dto.TotalPrice = cart.TotalPrice;
            foreach (OrderItem item in _orderService.GetCartItems(idUser))
            {
                CartItemDto line = new CartItemDto();
                line.IdProduct = item.IdProduct;
                line.ProductName = _productService.Get(item.IdProduct)?.Name;
                line.Quantity = item.Quantity;
                line.UnitPrice = item.UnitPrice;
                line.Total = item.Quantity * item.UnitPrice;
                dto.Items.Add(line);
            }
            return dto;
        }

        [HttpPost("items")]                     // POST /api/users/1/cart/items
        public ActionResult<CartDto> AddItem(int idUser, AddToCartRequest request)
        {
            _orderService.AddToCart(idUser, request.IdProduct, request.Quantity);
            return Get(idUser);                 // vraća izmenjenu korpu
        }

        [HttpDelete("items/{idProduct}")]       // DELETE /api/users/1/cart/items/3
        public ActionResult<CartDto> RemoveItem(int idUser, int idProduct)
        {
            _orderService.RemoveFromCart(idUser, idProduct);
            return Get(idUser);
        }

        [HttpPost("checkout")]                  // POST /api/users/1/cart/checkout
        public ActionResult<Order> Checkout(int idUser)
        {
            return _orderService.Checkout(idUser);
        }
    }
}
