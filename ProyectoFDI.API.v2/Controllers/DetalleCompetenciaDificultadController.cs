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
    public class DetalleCompetenciaDificultadController : ControllerBase
    {
        private readonly ProyectoFdiV2Context _context;

        public DetalleCompetenciaDificultadController(ProyectoFdiV2Context context)
        {
            _context = context;
        }

        // GET: api/DetalleCompetenciaDificultad
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleCompetenciaDificultad>>> GetDetalleCompetenciaDificultads()
        {
          if (_context.DetalleCompetenciaDificultads == null)
          {
              return NotFound();
          }

            return await _context.DetalleCompetenciaDificultads.Include("IdDepNavigation")
                .Include("IdComNavigation").ToListAsync();
        }

        // GET: api/DetalleCompetenciaDificultad/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleCompetenciaDificultad>> GetDetalleCompetenciaDificultad(int id)
        {
            var detalleCompetenciumDificultad = await _context.DetalleCompetenciaDificultads
                  .Where(x => x.IdDetalleDificultad == id)
                  .Include("IdDepNavigation").Include("IdComNavigation")
                  .ToListAsync();

            if (detalleCompetenciumDificultad == null)
            {
                return NotFound();
            }

            return detalleCompetenciumDificultad[0];
        }

        // PUT: api/DetalleCompetenciaDificultad/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleCompetenciaDificultad(int id, DetalleCompetenciaDificultad detalleCompetenciaDificultad)
        {
            if (id != detalleCompetenciaDificultad.IdDetalleDificultad)
            {
                return BadRequest();
            }

            _context.Entry(detalleCompetenciaDificultad).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleCompetenciaDificultadExists(id))
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

        // POST: api/DetalleCompetenciaDificultad
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DetalleCompetenciaDificultad>> PostDetalleCompetenciaDificultad(DetalleCompetenciaDificultad detalleCompetenciaDificultad)
        {
          if (_context.DetalleCompetenciaDificultads == null)
          {
              return Problem("Entity set 'ProyectoFdiV2Context.DetalleCompetenciaDificultads'  is null.");
          }
            _context.DetalleCompetenciaDificultads.Add(detalleCompetenciaDificultad);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetalleCompetenciaDificultad", new { id = detalleCompetenciaDificultad.IdDetalleDificultad }, detalleCompetenciaDificultad);
        }

        // DELETE: api/DetalleCompetenciaDificultad/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalleCompetenciaDificultad(int id)
        {
            if (_context.DetalleCompetenciaDificultads == null)
            {
                return NotFound();
            }
            var detalleCompetenciaDificultad = await _context.DetalleCompetenciaDificultads.FindAsync(id);
            if (detalleCompetenciaDificultad == null)
            {
                return NotFound();
            }

            _context.DetalleCompetenciaDificultads.Remove(detalleCompetenciaDificultad);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetalleCompetenciaDificultadExists(int id)
        {
            return (_context.DetalleCompetenciaDificultads?.Any(e => e.IdDetalleDificultad == id)).GetValueOrDefault();
        }
    }
}
