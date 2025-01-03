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
    public class VistaCompetenciumsController : ControllerBase
    {
        private readonly ProyectoFdiV2Context _context;

        public VistaCompetenciumsController(ProyectoFdiV2Context context)
        {
            _context = context;
        }

        // GET: api/VistaCompetenciums
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VistaCompetencium>>> GetVistaCompetencia()
        {
            if (_context.VistaCompetencia == null)
            {
                return NotFound();
            }
            return await _context.VistaCompetencia.ToListAsync();
        }

        // GET: api/VistaCompetenciums/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VistaCompetencium>> GetVistaCompetencium(int id)
        {
            if (_context.VistaCompetencia == null)
            {
                return NotFound();
            }
            var vistaCompetencium = await _context.VistaCompetencia.FindAsync(id);

            if (vistaCompetencium == null)
            {
                return NotFound();
            }

            return vistaCompetencium;
        }

        // PUT: api/VistaCompetenciums/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVistaCompetencium(int id, VistaCompetencium vistaCompetencium)
        {
            if (id != vistaCompetencium.IdCom)
            {
                return BadRequest();
            }

            _context.Entry(vistaCompetencium).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VistaCompetenciumExists(id))
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

        // POST: api/VistaCompetenciums
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VistaCompetencium>> PostVistaCompetencium(VistaCompetencium vistaCompetencium)
        {
            if (_context.VistaCompetencia == null)
            {
                return Problem("Entity set 'ProyectoFdiV2Context.VistaCompetencia'  is null.");
            }
            _context.VistaCompetencia.Add(vistaCompetencium);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (VistaCompetenciumExists(vistaCompetencium.IdCom))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetVistaCompetencium", new { id = vistaCompetencium.IdCom }, vistaCompetencium);
        }

        // DELETE: api/VistaCompetenciums/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVistaCompetencium(int id)
        {
            if (_context.VistaCompetencia == null)
            {
                return NotFound();
            }
            var vistaCompetencium = await _context.VistaCompetencia.FindAsync(id);
            if (vistaCompetencium == null)
            {
                return NotFound();
            }

            _context.VistaCompetencia.Remove(vistaCompetencium);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VistaCompetenciumExists(int id)
        {
            return (_context.VistaCompetencia?.Any(e => e.IdCom == id)).GetValueOrDefault();
        }
    }
}
