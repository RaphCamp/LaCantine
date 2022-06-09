using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LaCantine.Data;
using LaCantine.Model;

namespace LaCantine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatsController : ControllerBase
    {
        private readonly LaCantineContext _context;

        public PlatsController(LaCantineContext context)
        {
            _context = context;
        }

        // GET: api/Plats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Plats>>> GetPlats()
        {
            return await _context.Plats.ToListAsync();
        }

        // GET: api/Plats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Plats>> GetPlats(int id)
        {
            var plats = await _context.Plats.FindAsync(id);

            if (plats == null)
            {
                return NotFound();
            }

            return plats;
        }

        // PUT: api/Plats/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlats(int id, Plats plats)
        {
            if (id != plats.id)
            {
                return BadRequest();
            }

            _context.Entry(plats).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlatsExists(id))
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

        // POST: api/Plats
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Plats>> PostPlats(Plats plats)
        {
            _context.Plats.Add(plats);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlats", new { id = plats.id }, plats);
        }

        // DELETE: api/Plats/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlats(int id)
        {
            var plats = await _context.Plats.FindAsync(id);
            if (plats == null)
            {
                return NotFound();
            }

            _context.Plats.Remove(plats);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlatsExists(int id)
        {
            return _context.Plats.Any(e => e.id == id);
        }
    }
}
