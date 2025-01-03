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
    public class CompetenciaController : ControllerBase
    {
        private readonly ProyectoFdiV2Context _context;

        public CompetenciaController(ProyectoFdiV2Context context)
        {
            _context = context;
        }

        // GET: api/Competencia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Competencium>>> GetCompetencia(string? searchFor)
        {
            var datos = _context.Competencia.Include("DetalleCompetencia.IdDepNavigation")
                .Include("DetalleCompetenciaDificultads.IdDepNavigation");


            if (string.IsNullOrWhiteSpace(searchFor))
            {
                return await datos.ToListAsync();
            }
            else
            {
                return await datos.Where(p =>
                    p.NombreCom.ToLower().Contains(searchFor.ToLower()) ||
                    p.FechaInicioCom.ToString().Contains(searchFor) ||
                    p.IdCatNavigation.NombreCat.ToLower().Contains(searchFor.ToLower()) ||
                    p.FechaFinCom.ToString().Contains(searchFor) ||
                    p.IdJuezNavigation.NombresJuez.ToLower().Contains(searchFor.ToLower()) ||
                    p.IdGenNavigation.NombreGen.ToLower().Contains(searchFor.ToLower()) ||
                    p.IdJuezNavigation.ApellidosJuez.ToLower().Contains(searchFor.ToLower()) ||
                    p.IdModNavigation.DescripcionMod.ToLower().Contains(searchFor.ToLower()) ||
                    p.IdSedeNavigation.NombreSede.ToLower().Contains(searchFor.ToLower())

                ).ToListAsync();
            }
        }


        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Competencium>>> GetCompetencia(string? searchFor)
        //{
        //    return await _context.Competencia.ToListAsync();

        //}



        // GET: api/Competencia/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Competencium>> GetCompetencium(int id)
        {
            var competencium = await _context.Competencia
                .Where(x => x.IdCom == id)
                .Include("DetalleCompetencia.IdDepNavigation").Include("DetalleCompetencia")
                .Include("DetalleCompetenciaDificultads.IdDepNavigation").Include("DetalleCompetenciaDificultads")
                .ToListAsync();

            if (competencium == null)
            {
                return NotFound();
            }

            return competencium[0];
        }

        // PUT: api/Competencia/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompetencium(int id, Competencium competencium)
        {
            if (id != competencium.IdCom)
            {
                return BadRequest();
            }

            _context.Entry(competencium).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompetenciumExists(id))
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

        // POST: api/Competencia
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Competencium>> PostCompetencium(Competencium competencium)
        {
            _context.Competencia.Add(competencium);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompetencium", new { id = competencium.IdCom }, competencium);
        }

        // DELETE: api/Competencia/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompetencium(int id)
        {
            var competencium = await _context.Competencia.FindAsync(id);
            if (competencium == null)
            {
                return NotFound();
            }

            _context.Competencia.Remove(competencium);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompetenciumExists(int id)
        {
            return _context.Competencia.Any(e => e.IdCom == id);
        }
    }
}
