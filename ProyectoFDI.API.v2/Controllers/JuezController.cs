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
    public class JuezController : ControllerBase
    {
        private readonly ProyectoFdiV2Context _context;

        public JuezController(ProyectoFdiV2Context context)
        {
            _context = context;
        }

        // GET: api/Juez
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Juez>>> GetJuezs(string? searchFor)
        {
            var datos = _context.Juezs.Include("IdProNavigation").Include("IdUsuNavigation");

            if (string.IsNullOrWhiteSpace(searchFor))
            {
                return await datos.ToListAsync();
            }
            else
            {
                return await datos.Where(p =>
                    p.ApellidosJuez.ToLower().Contains(searchFor.ToLower()) ||
                    p.NombresJuez.ToLower().Contains(searchFor.ToLower()) ||
                    p.CedulaJuez.ToLower().Contains(searchFor.ToLower()) ||
                    p.IdProNavigation.NombrePro.ToLower().Contains(searchFor.ToLower()) ||
                    p.IdUsuNavigation.NombreUsu.ToLower().Contains(searchFor.ToLower())                
                ).ToListAsync();
            }
        }

        // GET: api/Juez/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Juez>> GetJuez(int id)
        {
            var juez = await _context.Juezs
                .Where(x => x.IdJuez == id)
                .Include("IdProNavigation").Include("IdUsuNavigation")
                .ToListAsync();

            if (juez == null)
            {
                return NotFound();
            }

            return juez[0];
        }

        // PUT: api/Juez/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJuez(int id, Juez juez)
        {
            if (id != juez.IdJuez)
            {
                return BadRequest();
            }

            _context.Entry(juez).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JuezExists(id))
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

        // POST: api/Juez
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Juez>> PostJuez(Juez juez)
        {
            _context.Juezs.Add(juez);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJuez", new { id = juez.IdJuez }, juez);
        }

        // DELETE: api/Juez/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJuez(int id)
        {
            var juez = await _context.Juezs.FindAsync(id);
            if (juez == null)
            {
                return NotFound();
            }

            _context.Juezs.Remove(juez);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JuezExists(int id)
        {
            return _context.Juezs.Any(e => e.IdJuez == id);
        }
    }
}
