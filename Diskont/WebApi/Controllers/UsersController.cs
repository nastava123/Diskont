using BusinessLayer;
using Entities;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]                 // adresa: /api/users
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly OrderService _orderService;

        public UsersController(UserService userService, OrderService orderService)
        {
            _userService = userService;
            _orderService = orderService;
        }

        [HttpGet]                               // GET /api/users
        public ActionResult<List<UserDto>> GetAll()
        {
            return _userService.GetAll().Select(UserDto.From).ToList();
        }

        [HttpGet("{id}")]                       // GET /api/users/1
        public ActionResult<UserDto> Get(int id)
        {
            User? user = _userService.Get(id);
            if (user == null)
                return NotFound(new { status = 404, message = "Korisnik ne postoji" });

            return UserDto.From(user);
        }

        [HttpPost]                              // POST /api/users  (registracija)
        public ActionResult Register(RegisterUserRequest request)
        {
            User user = new User();
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.PasswordHash = request.Password;   

            _userService.Add(user);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpGet("{id}/orders")]                // GET /api/users/1/orders
        public ActionResult<List<Order>> GetOrders(int id)
        {
            if (_userService.Get(id) == null)
                return NotFound(new { status = 404, message = "Korisnik ne postoji" });

            return _orderService.GetHistory(id);
        }
    }
}
