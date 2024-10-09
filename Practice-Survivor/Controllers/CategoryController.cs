using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practice_Survivor.Data;
using Practice_Survivor.Enitities;
using Practice_Survivor.Enitity;

namespace Practice_Survivor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly SurvivorDbContext _context;
        public CategoryController(SurvivorDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var categories = _context.Categories.Include(c => c.Competitors).ToList();

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpPost]
        public IActionResult Add([FromBody] CategoryEntity category)
        {
            if (category is null)
                return BadRequest();

            _context.Categories.Add(category);



            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CategoryEntity category)
        {
            if (category is null || id != category.Id)
                return BadRequest();
            var oldCategory = _context.Categories.FirstOrDefault(c => c.Id == id);

            if (oldCategory is null)
                return NotFound();

            oldCategory.ModifiedDate = category.ModifiedDate;
            oldCategory.Name = category.Name;


            _context.SaveChanges();

            return Ok(oldCategory);


        }

        [HttpDelete("{id}")]

        public IActionResult DeleteById(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category is null)
                return NotFound();
            category.IsDeleted = true;
            _context.SaveChanges();
            return Ok();
        }

    }
}
