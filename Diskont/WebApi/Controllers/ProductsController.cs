using BusinessLayer;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]                 // adresa: /api/products
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]                               // GET /api/products?idCategory=2
        public ActionResult<List<Product>> GetAll(int? idCategory)
        {
            if (idCategory.HasValue)
                return _productService.GetByCategory(idCategory.Value);

            return _productService.GetAll();
        }

        [HttpGet("available")]                  // GET /api/products/available
        public ActionResult<List<Product>> GetAvailable()
        {
            return _productService.GetAvailable();
        }

        [HttpGet("{id}")]                       // GET /api/products/3
        public ActionResult<Product> Get(int id)
        {
            Product? product = _productService.Get(id);
            if (product == null)
                return NotFound(new { status = 404, message = "Proizvod ne postoji" });

            return product;
        }

        [HttpPost]                              // POST /api/products
        public ActionResult Add(Product product)
        {
            _productService.Add(product);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]                       // PUT /api/products/3
        public ActionResult Update(int id, Product product)
        {
            if (id != product.IdProduct)
                return BadRequest(new { status = 400,
                                        message = "ID u adresi i u telu se razlikuju" });

            _productService.Update(product);
            return NoContent();
        }

        [HttpDelete("{id}")]                    // DELETE /api/products/3
        public ActionResult Delete(int id)
        {
            Product? product = _productService.Get(id);
            if (product == null)
                return NotFound(new { status = 404, message = "Proizvod ne postoji" });

            _productService.Delete(product);
            return NoContent();
        }
    }
}
