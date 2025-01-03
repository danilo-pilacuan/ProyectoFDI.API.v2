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
    public class DetalleCompetenciaController : ControllerBase
    {
        private readonly ProyectoFdiV2Context _context;

        public DetalleCompetenciaController(ProyectoFdiV2Context context)
        {
            _context = context;
        }

        // GET: api/DetalleCompetencia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleCompetencium>>> GetDetalleCompetencia()
        {
            return await _context.DetalleCompetencia.Include("IdDepNavigation")
                .Include("IdComNavigation").ToListAsync();
        }

        // GET: api/DetalleCompetencia/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleCompetencium>> GetDetalleCompetencium(int id)
        {
            var detalleCompetencium = await _context.DetalleCompetencia
                .Where(x => x.IdDetalle == id)
                .Include("IdDepNavigation").Include("IdComNavigation")
                .ToListAsync();

            if (detalleCompetencium == null)
            {
                return NotFound();
            }

            return detalleCompetencium[0];
        }

        // PUT: api/DetalleCompetencia/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleCompetencium(int id, DetalleCompetencium detalleCompetencium)
        {
            if (id != detalleCompetencium.IdDetalle)
            {
                return BadRequest();
            }

            _context.Entry(detalleCompetencium).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleCompetenciumExists(id))
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

        // POST: api/DetalleCompetencia
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DetalleCompetencium>> PostDetalleCompetencium(DetalleCompetencium detalleCompetencium)
        {
            _context.DetalleCompetencia.Add(detalleCompetencium);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetalleCompetencium", new { id = detalleCompetencium.IdDetalle }, detalleCompetencium);
        }

        // DELETE: api/DetalleCompetencia/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalleCompetencium(int id)
        {
            var detalleCompetencium = await _context.DetalleCompetencia.FindAsync(id);
            if (detalleCompetencium == null)
            {
                return NotFound();
            }

            _context.DetalleCompetencia.Remove(detalleCompetencium);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetalleCompetenciumExists(int id)
        {
            return _context.DetalleCompetencia.Any(e => e.IdDetalle == id);
        }
    }
}
