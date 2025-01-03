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
    public class VistaViasResultadoesController : ControllerBase
    {
        private readonly ProyectoFdiV2Context _context;

        public VistaViasResultadoesController(ProyectoFdiV2Context context)
        {
            _context = context;
        }

        // GET: api/VistaViasResultadoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VistaViasResultado>>> GetVistaViasResultados()
        {
          if (_context.VistaViasResultados == null)
          {
              return NotFound();
          }
            return await _context.VistaViasResultados.ToListAsync();
        }

        // GET: api/VistaViasResultadoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<VistaViasResultado>>> GetVistaViasResultado(int id)
        {
            if (_context.VistaViasResultados == null)
            {
                return NotFound();
            }
            var vistaViasResultado = _context.VistaViasResultados;



            if (vistaViasResultado == null)
            {
                return NotFound();
            }

            return await vistaViasResultado.Where(p => p.IdCompe == id).ToListAsync();

        }

        // PUT: api/VistaViasResultadoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVistaViasResultado(int id, VistaViasResultado vistaViasResultado)
        {
            if (id != vistaViasResultado.IdCompe)
            {
                return BadRequest();
            }

            _context.Entry(vistaViasResultado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VistaViasResultadoExists(id))
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

        // POST: api/VistaViasResultadoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VistaViasResultado>> PostVistaViasResultado(VistaViasResultado vistaViasResultado)
        {
          if (_context.VistaViasResultados == null)
          {
              return Problem("Entity set 'ProyectoFdiV2Context.VistaViasResultados'  is null.");
          }
            _context.VistaViasResultados.Add(vistaViasResultado);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (VistaViasResultadoExists(vistaViasResultado.IdCompe))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetVistaViasResultado", new { id = vistaViasResultado.IdCompe }, vistaViasResultado);
        }

        // DELETE: api/VistaViasResultadoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVistaViasResultado(int id)
        {
            if (_context.VistaViasResultados == null)
            {
                return NotFound();
            }
            var vistaViasResultado = await _context.VistaViasResultados.FindAsync(id);
            if (vistaViasResultado == null)
            {
                return NotFound();
            }

            _context.VistaViasResultados.Remove(vistaViasResultado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VistaViasResultadoExists(int id)
        {
            return (_context.VistaViasResultados?.Any(e => e.IdCompe == id)).GetValueOrDefault();
        }
    }
}
