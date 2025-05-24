using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WalkingAPI.Models.Domain;

namespace WalkingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DifficultyController : ControllerBase
    {
        private readonly WalkDbContext _context;

        public DifficultyController(WalkDbContext context)
        {
            _context = context;
        }

        // GET: api/Difficulty
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Difficulty>>> GetDifficulties()
        {
            return await _context.Difficulties.ToListAsync();
        }

        // GET: api/Difficulty/5
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<Difficulty>> GetDifficulty(Guid id)
        {
            var difficulty = await _context.Difficulties.FindAsync(id);

            if (difficulty == null)
            {
                return NotFound();
            }

            return difficulty;
        }

        // PUT: api/Difficulty/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> PutDifficulty(Guid id, Difficulty difficulty)
        {
            if (id != difficulty.Id)
            {
                return BadRequest();
            }

            _context.Entry(difficulty).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DifficultyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Difficulty
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Difficulty>> PostDifficulty(Difficulty difficulty)
        {
            _context.Difficulties.Add(difficulty);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDifficulty", new { id = difficulty.Id }, difficulty);
        }

        // DELETE: api/Difficulty/5
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteDifficulty(Guid id)
        {
            var difficulty = await _context.Difficulties.FindAsync(id);
            if (difficulty == null)
            {
                return NotFound();
            }

            _context.Difficulties.Remove(difficulty);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DifficultyExists(Guid id)
        {
            return _context.Difficulties.Any(e => e.Id == id);
        }
    }
}
