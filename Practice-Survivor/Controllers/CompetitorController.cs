using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practice_Survivor.Data;
using Practice_Survivor.Enitity;

namespace Practice_Survivor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompetitorController : ControllerBase
    {
        private readonly SurvivorDbContext _context;
        public CompetitorController(SurvivorDbContext context)
        {
            _context = context;
        }

        //yarışmacıların hepsini geri döndüren endpoint
        [HttpGet]
        public IActionResult GetAll()
        {
            var competitors = _context.Competitor.Include(c => c.Category).ToList();

            return Ok(competitors);
        }

        // girilen Id 'ye sahip olan yarışmacıyı döndürür
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var competitor = _context.Competitor.FirstOrDefault(c => c.Id == id);

            if (competitor == null)
            {
                return NotFound();
            }
            return Ok(competitor);
        }

        // girilen kategori Id'ye göre olan yarışmacıları filtreler

        [HttpGet("categories/{categoryId}")]
        public IActionResult GetCompetitorsByCategoryId(int categoryId)
        {
            var competitors = _context.Competitor
                                                 .Include(c => c.Category)
                                                 .Where(c=> c.CategoryId == categoryId)
                                                 .ToList();
            if(competitors is null || !competitors.Any())
                return NotFound();
            return Ok(competitors);
        }
        [HttpPost]
        public IActionResult Add([FromBody] CompetitorEntity competitor)
        {
            if (competitor is null)
                return BadRequest();

            _context.Competitor.Add(competitor);

            

            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new {id = competitor.Id},competitor);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CompetitorEntity competitor)
        {
            if(competitor is null || id != competitor.Id)
                return BadRequest();
            var oldCompetitor = _context.Competitor.FirstOrDefault(c=> c.Id == id);

            if(oldCompetitor is null)
                return NotFound();

            oldCompetitor.ModifiedDate = competitor.ModifiedDate;
            oldCompetitor.CategoryId = competitor.CategoryId;
            oldCompetitor.FirstName = competitor.FirstName;
            oldCompetitor.LastName = competitor.LastName;

            _context.SaveChanges();

            return Ok(oldCompetitor);


        }
        [HttpDelete("{id}")]

        public IActionResult DeleteById(int id)
        {
            var competitor = _context.Competitor.FirstOrDefault( c=> c.Id == id);
            if(competitor is null)
                return NotFound();
            competitor.IsDeleted = true;
            _context.SaveChanges();
            return Ok();
        }

    }
}
