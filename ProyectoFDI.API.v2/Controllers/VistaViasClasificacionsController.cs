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
    public class VistaViasClasificacionsController : ControllerBase
    {
        private readonly ProyectoFdiV2Context _context;

        public VistaViasClasificacionsController(ProyectoFdiV2Context context)
        {
            _context = context;
        }

        // GET: api/VistaViasClasificacions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VistaViasClasificacion>>> GetVistaViasClasificacions()
        {
          if (_context.VistaViasClasificacions == null)
          {
              return NotFound();
          }
            return await _context.VistaViasClasificacions.ToListAsync();
        }

        // GET: api/VistaViasClasificacions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<VistaViasClasificacion>>> GetVistaViasClasificacion(int id)
        {
          if (_context.VistaViasClasificacions == null)
          {
              return NotFound();
          }
            var vistaViasClasificacion = _context.VistaViasClasificacions;



            if (vistaViasClasificacion == null)
            {
                return NotFound();
            }

            return await vistaViasClasificacion.Where(p => p.IdCompe == id).ToListAsync();
        }

        // PUT: api/VistaViasClasificacions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVistaViasClasificacion(int id, VistaViasClasificacion vistaViasClasificacion)
        {
            if (id != vistaViasClasificacion.IdCompe)
            {
                return BadRequest();
            }

            _context.Entry(vistaViasClasificacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VistaViasClasificacionExists(id))
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

        // POST: api/VistaViasClasificacions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VistaViasClasificacion>> PostVistaViasClasificacion(VistaViasClasificacion vistaViasClasificacion)
        {
          if (_context.VistaViasClasificacions == null)
          {
              return Problem("Entity set 'ProyectoFdiV2Context.VistaViasClasificacions'  is null.");
          }
            _context.VistaViasClasificacions.Add(vistaViasClasificacion);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (VistaViasClasificacionExists(vistaViasClasificacion.IdCompe))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetVistaViasClasificacion", new { id = vistaViasClasificacion.IdCompe }, vistaViasClasificacion);
        }

        // DELETE: api/VistaViasClasificacions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVistaViasClasificacion(int id)
        {
            if (_context.VistaViasClasificacions == null)
            {
                return NotFound();
            }
            var vistaViasClasificacion = await _context.VistaViasClasificacions.FindAsync(id);
            if (vistaViasClasificacion == null)
            {
                return NotFound();
            }

            _context.VistaViasClasificacions.Remove(vistaViasClasificacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VistaViasClasificacionExists(int id)
        {
            return (_context.VistaViasClasificacions?.Any(e => e.IdCompe == id)).GetValueOrDefault();
        }
    }
}
