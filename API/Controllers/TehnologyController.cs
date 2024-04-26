using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechnologyController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public TechnologyController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

    
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Technology>>> GetTechnologies()
        {
            var technologies = await _dbContext.Technology.ToListAsync();
            if (technologies == null || !technologies.Any())
            {
                return NotFound();
            }

            return technologies;
        }

    
        [HttpGet("{id}")]
        public async Task<ActionResult<Technology>> GetTechnology(int id)
        {
            var technology = await _dbContext.Technology.FindAsync(id);
            if (technology == null)
            {
                return NotFound();
            }

            return technology;
        }

        
        [HttpPost]
        public async Task<ActionResult<Technology>> PostTechnology(PostTechnology postTechnology)
        {
            var technology = new Technology
            {
                Name = postTechnology.Name,
                SkillLevel = postTechnology.SkillLevel
            };

            _dbContext.Technology.Add(technology);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTechnology), new { id = technology.Id }, technology);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTechnology(int id, PostTechnology postTechnology)
        {
         
            var existingTechnology = await _dbContext.Technology.FindAsync(id);
            if (existingTechnology == null)
            {
                return NotFound();
            }

            existingTechnology.Name = postTechnology.Name;
            existingTechnology.SkillLevel = postTechnology.SkillLevel;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TechnologyExists(id))
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

      
        [HttpDelete("{id}")]
        public async Task<ActionResult<Technology>> DeleteTechnology(int id)
        {
            var technology = await _dbContext.Technology.FindAsync(id);
            if (technology == null)
            {
                return NotFound();
            }

            _dbContext.Technology.Remove(technology);
            await _dbContext.SaveChangesAsync();

            return technology;
        }

        private bool TechnologyExists(int id)
        {
            return _dbContext.Technology.Any(e => e.Id == id);
        }
    }
}
