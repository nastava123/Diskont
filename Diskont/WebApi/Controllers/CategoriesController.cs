using BusinessLayer;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]                 // adresa: /api/categories
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoriesController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]                               // GET /api/categories
        public ActionResult<List<Category>> GetAll()
        {
            return _categoryService.GetAll();
        }

        [HttpGet("{id}")]                       // GET /api/categories/2
        public ActionResult<Category> Get(int id)
        {
            Category? category = _categoryService.Get(id);
            if (category == null)
                return NotFound(new { status = 404, message = "Kategorija ne postoji" });

            return category;                    // 200 + JSON
        }

        [HttpPost]                              // POST /api/categories
        public ActionResult Add(Category category)
        {
            _categoryService.Add(category);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]                       // PUT /api/categories/2
        public ActionResult Update(int id, Category category)
        {
            if (id != category.IdCategory)
                return BadRequest(new { status = 400,
                                        message = "ID u adresi i u telu se razlikuju" });

            _categoryService.Update(category);
            return NoContent();                 // 204
        }

        [HttpDelete("{id}")]                    // DELETE /api/categories/2
        public ActionResult Delete(int id)
        {
            Category? category = _categoryService.Get(id);
            if (category == null)
                return NotFound(new { status = 404, message = "Kategorija ne postoji" });

            _categoryService.Delete(category);
            return NoContent();
        }
    }
}
