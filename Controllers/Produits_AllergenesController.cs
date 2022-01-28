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
    public class Produits_AllergenesController : ControllerBase
    {
        private readonly LaCantineContext _context;

        public Produits_AllergenesController(LaCantineContext context)
        {
            _context = context;
        }

        // GET: api/Produits_Allergenes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produits_Allergenes>>> GetProduits_Allergenes()
        {
            return await _context.Produits_Allergenes.ToListAsync();
        }

        // GET: api/Produits_Allergenes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Produits_Allergenes>> GetProduits_Allergenes(int id)
        {
            var produits_Allergenes = await _context.Produits_Allergenes.FindAsync(id);

            if (produits_Allergenes == null)
            {
                return NotFound();
            }

            return produits_Allergenes;
        }

        // PUT: api/Produits_Allergenes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduits_Allergenes(int id, Produits_Allergenes produits_Allergenes)
        {
            if (id != produits_Allergenes.ID)
            {
                return BadRequest();
            }

            _context.Entry(produits_Allergenes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Produits_AllergenesExists(id))
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

        // POST: api/Produits_Allergenes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Produits_Allergenes>> PostProduits_Allergenes(Produits_Allergenes produits_Allergenes)
        {
            _context.Produits_Allergenes.Add(produits_Allergenes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduits_Allergenes", new { id = produits_Allergenes.ID }, produits_Allergenes);
        }

        // DELETE: api/Produits_Allergenes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduits_Allergenes(int id)
        {
            var produits_Allergenes = await _context.Produits_Allergenes.FindAsync(id);
            if (produits_Allergenes == null)
            {
                return NotFound();
            }

            _context.Produits_Allergenes.Remove(produits_Allergenes);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Produits_AllergenesExists(int id)
        {
            return _context.Produits_Allergenes.Any(e => e.ID == id);
        }
    }
}
