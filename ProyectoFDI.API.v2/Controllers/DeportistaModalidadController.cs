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
    public class DeportistaModalidadController : ControllerBase
    {
        private readonly ProyectoFdiV2Context _context;

        public DeportistaModalidadController(ProyectoFdiV2Context context)
        {
            _context = context;
        }

        // GET: api/DeportistaModalidad
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeportistaModalidad>>> GetDeportistaModalidads()
        {
            return await _context.DeportistaModalidads.Include("IdModNavigation").ToListAsync();
        }

        // GET: api/DeportistaModalidad/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DeportistaModalidad>> GetDeportistaModalidad(int id)
        {
            var deportistaModalidad = await _context.DeportistaModalidads.FindAsync(id);

            if (deportistaModalidad == null)
            {
                return NotFound();
            }

            return deportistaModalidad;
        }

        // PUT: api/DeportistaModalidad/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeportistaModalidad(int id, DeportistaModalidad deportistaModalidad)
        {
            if (id != deportistaModalidad.IdDepmod)
            {
                return BadRequest();
            }

            _context.Entry(deportistaModalidad).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeportistaModalidadExists(id))
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

        // POST: api/DeportistaModalidad
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DeportistaModalidad>> PostDeportistaModalidad(DeportistaModalidad deportistaModalidad)
        {
            _context.DeportistaModalidads.Add(deportistaModalidad);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDeportistaModalidad", new { id = deportistaModalidad.IdDepmod }, deportistaModalidad);
        }

        // DELETE: api/DeportistaModalidad/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeportistaModalidad(int id)
        {
            var deportistaModalidad = await _context.DeportistaModalidads.FindAsync(id);
            if (deportistaModalidad == null)
            {
                return NotFound();
            }

            _context.DeportistaModalidads.Remove(deportistaModalidad);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DeportistaModalidadExists(int id)
        {
            return _context.DeportistaModalidads.Any(e => e.IdDepmod == id);
        }
    }
}
