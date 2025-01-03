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
    public class VistaVeloFinalsController : ControllerBase
    {
        private readonly ProyectoFdiV2Context _context;

        public VistaVeloFinalsController(ProyectoFdiV2Context context)
        {
            _context = context;
        }

        // GET: api/VistaVeloFinals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VistaVeloFinal>>> GetVistaVeloFinals()
        {
          if (_context.VistaVeloFinals == null)
          {
              return NotFound();
          }
            return await _context.VistaVeloFinals.ToListAsync();
        }

        // GET: api/VistaVeloFinals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<VistaVeloFinal>>> GetVistaVeloFinal(int id)
        {
          if (_context.VistaVeloFinals == null)
          {
              return NotFound();
          }
            var vistaVeloFinal = _context.VistaVeloFinals;



            if (vistaVeloFinal == null)
            {
                return NotFound();
            }

            return await vistaVeloFinal.Where(p => p.IdCompe == id).ToListAsync();
        }

        // PUT: api/VistaVeloFinals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVistaVeloFinal(int id, VistaVeloFinal vistaVeloFinal)
        {
            if (id != vistaVeloFinal.IdCompe)
            {
                return BadRequest();
            }

            _context.Entry(vistaVeloFinal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VistaVeloFinalExists(id))
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

        // POST: api/VistaVeloFinals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VistaVeloFinal>> PostVistaVeloFinal(VistaVeloFinal vistaVeloFinal)
        {
          if (_context.VistaVeloFinals == null)
          {
              return Problem("Entity set 'ProyectoFdiV2Context.VistaVeloFinals'  is null.");
          }
            _context.VistaVeloFinals.Add(vistaVeloFinal);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (VistaVeloFinalExists(vistaVeloFinal.IdCompe))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetVistaVeloFinal", new { id = vistaVeloFinal.IdCompe }, vistaVeloFinal);
        }

        // DELETE: api/VistaVeloFinals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVistaVeloFinal(int id)
        {
            if (_context.VistaVeloFinals == null)
            {
                return NotFound();
            }
            var vistaVeloFinal = await _context.VistaVeloFinals.FindAsync(id);
            if (vistaVeloFinal == null)
            {
                return NotFound();
            }

            _context.VistaVeloFinals.Remove(vistaVeloFinal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VistaVeloFinalExists(int id)
        {
            return (_context.VistaVeloFinals?.Any(e => e.IdCompe == id)).GetValueOrDefault();
        }
    }
}
