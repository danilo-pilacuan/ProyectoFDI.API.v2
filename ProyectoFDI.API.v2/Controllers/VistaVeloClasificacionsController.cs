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
    public class VistaVeloClasificacionsController : ControllerBase
    {
        private readonly ProyectoFdiV2Context _context;

        public VistaVeloClasificacionsController(ProyectoFdiV2Context context)
        {
            _context = context;
        }

        // GET: api/VistaVeloClasificacions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VistaVeloClasificacion>>> GetVistaVeloClasificacions()
        {
          if (_context.VistaVeloClasificacions == null)
          {
              return NotFound();
          }
            return await _context.VistaVeloClasificacions.ToListAsync();
        }

        // GET: api/VistaVeloClasificacions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<VistaVeloClasificacion>>> GetVistaVeloClasificacion(int id)
        {
          if (_context.VistaVeloClasificacions == null)
          {
              return NotFound();
          }
            var vistaVeloClasificacion = _context.VistaVeloClasificacions;


            if (vistaVeloClasificacion == null)
            {
                return NotFound();
            }

            return await vistaVeloClasificacion.Where(p => p.IdCompe == id).ToListAsync();
        }

        // PUT: api/VistaVeloClasificacions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVistaVeloClasificacion(int id, VistaVeloClasificacion vistaVeloClasificacion)
        {
            if (id != vistaVeloClasificacion.IdCompe)
            {
                return BadRequest();
            }

            _context.Entry(vistaVeloClasificacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VistaVeloClasificacionExists(id))
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

        // POST: api/VistaVeloClasificacions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VistaVeloClasificacion>> PostVistaVeloClasificacion(VistaVeloClasificacion vistaVeloClasificacion)
        {
          if (_context.VistaVeloClasificacions == null)
          {
              return Problem("Entity set 'ProyectoFdiV2Context.VistaVeloClasificacions'  is null.");
          }
            _context.VistaVeloClasificacions.Add(vistaVeloClasificacion);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (VistaVeloClasificacionExists(vistaVeloClasificacion.IdCompe))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetVistaVeloClasificacion", new { id = vistaVeloClasificacion.IdCompe }, vistaVeloClasificacion);
        }

        // DELETE: api/VistaVeloClasificacions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVistaVeloClasificacion(int id)
        {
            if (_context.VistaVeloClasificacions == null)
            {
                return NotFound();
            }
            var vistaVeloClasificacion = await _context.VistaVeloClasificacions.FindAsync(id);
            if (vistaVeloClasificacion == null)
            {
                return NotFound();
            }

            _context.VistaVeloClasificacions.Remove(vistaVeloClasificacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VistaVeloClasificacionExists(int id)
        {
            return (_context.VistaVeloClasificacions?.Any(e => e.IdCompe == id)).GetValueOrDefault();
        }
    }
}
