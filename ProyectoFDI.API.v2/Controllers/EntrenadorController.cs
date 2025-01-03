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
    public class EntrenadorController : ControllerBase
    {
        private readonly ProyectoFdiV2Context _context;

        public EntrenadorController(ProyectoFdiV2Context context)
        {
            _context = context;
        }

        // GET: api/Entrenador
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Entrenador>>> GetEntrenadors(string? searchFor)
        {
            var datos = _context.Entrenadors.Include("IdProNavigation").Include("Deportista").
                Include("IdUsuNavigation");

            if (string.IsNullOrWhiteSpace(searchFor))
            {
                return await datos.ToListAsync();
            }
            else
            {
                return await datos.Where(p =>
                    p.NombresEnt.ToLower().Contains(searchFor.ToLower()) ||
                    p.ApellidosEnt.ToLower().Contains(searchFor.ToLower()) ||
                    p.CedulaEnt.ToLower().Contains(searchFor.ToLower()) ||
                    p.IdProNavigation.NombrePro.ToLower().Contains(searchFor.ToLower()) ||
                    p.IdUsuNavigation.NombreUsu.ToLower().Contains(searchFor.ToLower())

                ).ToListAsync();
            }
        }

        // GET: api/Entrenador/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Entrenador>> GetEntrenador(int id)
        {
            var entrenador = await _context
                .Entrenadors
                .Where(x => x.IdEnt == id)
                .Include("IdProNavigation").Include("Deportista").
                Include("IdUsuNavigation")
                .ToListAsync();

            if (entrenador == null)
            {
                return NotFound();
            }

            return entrenador[0];
        }

        // PUT: api/Entrenador/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEntrenador(int id, Entrenador entrenador)
        {
            if (id != entrenador.IdEnt)
            {
                return BadRequest();
            }

            _context.Entry(entrenador).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntrenadorExists(id))
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

        // POST: api/Entrenador
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Entrenador>> PostEntrenador(Entrenador entrenador)
        {
            _context.Entrenadors.Add(entrenador);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEntrenador", new { id = entrenador.IdEnt }, entrenador);
        }

        // DELETE: api/Entrenador/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntrenador(int id)
        {
            var entrenador = await _context.Entrenadors.FindAsync(id);
            if (entrenador == null)
            {
                return NotFound();
            }

            _context.Entrenadors.Remove(entrenador);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EntrenadorExists(int id)
        {
            return _context.Entrenadors.Any(e => e.IdEnt == id);
        }
    }
}
