#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CCDC.Models;

namespace CCDC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PalItemsController : ControllerBase
    {
        private readonly PalContext _context;

        public PalItemsController(PalContext context)
        {
            _context = context;
        }

        // GET: api/PalItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PalItem>>> GetPalindromes()
        {
            return await _context.Palindromes.ToListAsync();
        }

        // GET: api/PalItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PalItem>> GetPalItem(long id)
        {
            var palItem = await _context.Palindromes.FindAsync(id);

            if (palItem == null)
            {
                return NotFound();
            }

            return palItem;
        }

        // PUT: api/PalItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPalItem(long id, PalItem palItem)
        {
            if (id != palItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(palItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PalItemExists(id))
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

        // POST: api/PalItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PalItem>> PostPalItem(PalItem palItem)
        {
            _context.Palindromes.Add(palItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPalItem", new { id = palItem.Id }, palItem);
        }

        // DELETE: api/PalItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePalItem(long id)
        {
            var palItem = await _context.Palindromes.FindAsync(id);
            if (palItem == null)
            {
                return NotFound();
            }

            _context.Palindromes.Remove(palItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PalItemExists(long id)
        {
            return _context.Palindromes.Any(e => e.Id == id);
        }
    }
}
