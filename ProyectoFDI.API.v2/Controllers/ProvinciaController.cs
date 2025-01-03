using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFDI.API.v2.ModelsV4;

namespace ProyectoFDI.API.v2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvinciaController : ControllerBase
    {
        private readonly ProyectoFdiV2Context _context;

        public ProvinciaController(ProyectoFdiV2Context context)
        {
            _context = context;
        }

        // GET: api/Provincia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Provincium>>> GetProvincia()
        {
            return await _context.Provincia.ToListAsync();
        }

        // GET: api/Provincia/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Provincium>> GetProvincium(int id)
        {
            var provincium = await _context.Provincia.FindAsync(id);

            if (provincium == null)
            {
                return NotFound();
            }

            return provincium;
        }

        // PUT: api/Provincia/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProvincium(int id, Provincium provincium)
        {
            if (id != provincium.IdPro)
            {
                return BadRequest();
            }

            _context.Entry(provincium).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProvinciumExists(id))
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

        // POST: api/Provincia
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Provincium>> PostProvincium(Provincium provincium)
        {
            _context.Provincia.Add(provincium);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProvincium", new { id = provincium.IdPro }, provincium);
        }

        // DELETE: api/Provincia/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProvincium(int id)
        {
            var provincium = await _context.Provincia.FindAsync(id);
            if (provincium == null)
            {
                return NotFound();
            }

            _context.Provincia.Remove(provincium);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProvinciumExists(int id)
        {
            return _context.Provincia.Any(e => e.IdPro == id);
        }
    }
}
