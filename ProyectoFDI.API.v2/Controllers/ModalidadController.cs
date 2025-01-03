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
    public class ModalidadController : ControllerBase
    {
        private readonly ProyectoFdiV2Context _context;

        public ModalidadController(ProyectoFdiV2Context context)
        {
            _context = context;
        }

        // GET: api/Modalidad
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Modalidad>>> GetModalidads()
        {
            return await _context.Modalidads.ToListAsync();
        }

        // GET: api/Modalidad/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Modalidad>> GetModalidad(int id)
        {
            var modalidad = await _context.Modalidads.FindAsync(id);

            if (modalidad == null)
            {
                return NotFound();
            }

            return modalidad;
        }

        // PUT: api/Modalidad/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModalidad(int id, Modalidad modalidad)
        {
            if (id != modalidad.IdMod)
            {
                return BadRequest();
            }

            _context.Entry(modalidad).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModalidadExists(id))
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

        // POST: api/Modalidad
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Modalidad>> PostModalidad(Modalidad modalidad)
        {
            _context.Modalidads.Add(modalidad);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetModalidad", new { id = modalidad.IdMod }, modalidad);
        }

        // DELETE: api/Modalidad/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModalidad(int id)
        {
            var modalidad = await _context.Modalidads.FindAsync(id);
            if (modalidad == null)
            {
                return NotFound();
            }

            _context.Modalidads.Remove(modalidad);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ModalidadExists(int id)
        {
            return _context.Modalidads.Any(e => e.IdMod == id);
        }
    }
}
