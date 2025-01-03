using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using ProyectoFDI.API.v2.ModelsV4;

namespace ProyectoFDI.API.v2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeportistaController : ControllerBase
    {
        private readonly ProyectoFdiV2Context _context;

        public DeportistaController(ProyectoFdiV2Context context)
        {
            _context = context;
        }

        // GET: api/Deportista
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Deportistum>>> GetDeportista(string? searchFor)
        {
            var datos = _context.Deportista;

            if (string.IsNullOrWhiteSpace(searchFor))
            {
                return await datos.ToListAsync();
            }
            else
            {
                return await datos.Where(p =>
                    p.ApellidosDep.ToLower().Contains(searchFor.ToLower()) ||
                    p.CedulaDep.ToLower().Contains(searchFor.ToLower()) ||
                    p.IdCatNavigation.NombreCat.ToLower().Contains(searchFor.ToLower()) ||
                    p.IdClubNavigation.NombreClub.ToLower().Contains(searchFor.ToLower()) ||
                    p.IdEntNavigation.NombresEnt.ToLower().Contains(searchFor.ToLower()) ||
                    p.IdGenNavigation.NombreGen.ToLower().Contains(searchFor.ToLower()) ||
                    p.IdUsuNavigation.NombreUsu.ToLower().Contains(searchFor.ToLower()) ||
                    p.NombresDep.ToLower().Contains(searchFor.ToLower()) ||
                    p.IdProNavigation.NombrePro.ToLower().Contains(searchFor.ToLower())

                ).ToListAsync();
            }

            //return await _context.Deportista
            //    .ToListAsync();
        }

        [HttpGet("{genero}-{categoria}")]
        public async Task<ActionResult<IEnumerable<Deportistum>>> GetDeportista(int genero, int categoria)
        {
            var datos = _context.Deportista;

            if (genero != null && categoria != null)
            {
                return await datos.Where(p => p.IdCat == categoria &&
                    p.IdGen == genero).ToListAsync();
            }
            else
            {
                return null;

            }

            //return await _context.Deportista
            //    .ToListAsync();
        }
        /*
        [HttpGet("competencia/{id}")]
        public async Task<ActionResult<IEnumerable<Deportistum>>> GetDeportista(int id)
        {
            

            if (id != null)
            {
                var detallesBloque = await _context.CompetenciaBloqueClasificas
                    .Where(p => p.IdCom == id).ToListAsync();

                var deportistaIds = detallesBloque
                    .Select(dc => dc.IdDep).ToList();

                return await _context.Deportista
                    .Where(d => deportistaIds.Contains(d.IdDep))
                    .ToListAsync();
            }
            else
            {
                return null;

            }
        }*/

        // GET: api/Deportista/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Deportistum>> GetDeportistum(int id)
        {
            var deportistum = await _context
                .Deportista
                .Where(x => x.IdDep == id)
                .ToListAsync();

            if (deportistum == null)
            {
                return NotFound();
            }

            return deportistum[0];
        }
        [HttpGet("competencia/{id}/{etapa}")]
        public async Task<ActionResult<IEnumerable<Deportistum>>> GetDeportistasPorCompetencia(int id,String etapa)
        {
            // Verificar si la competencia existe
            var competencia = await _context.Competencia.FindAsync(id);
            if (competencia == null)
            {
                return NotFound("Competencia no encontrada");
            }

            // Consultar los IDs de los deportistas asignados a esta competencia a través de la tabla ResultadoBloque
            var idsDeportistasAsignados = await _context.ResultadoBloques
                .Where(rb => rb.IdCom == id &  rb.Etapa== etapa )
                .Select(rb => rb.IdDep)
                .ToListAsync();

            if (idsDeportistasAsignados.Count == 0)
            {
                return NotFound("No se encontraron deportistas asignados a esta competencia");
            }

            // Consultar los deportistas asignados usando los IDs obtenidos
            var deportistasAsignados = await _context.Deportista
                .Where(d => idsDeportistasAsignados.Contains(d.IdDep))
                .ToListAsync();

            return deportistasAsignados;
        }


        // PUT: api/Deportista/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeportistum(int id, Deportistum deportistum)
        {
            if (id != deportistum.IdDep)
            {
                return BadRequest();
            }

            _context.Entry(deportistum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeportistumExists(id))
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

        // POST: api/Deportista
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Deportistum>> PostDeportistum(Deportistum deportistum)
        {
            _context.Deportista.Add(deportistum);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDeportistum", new { id = deportistum.IdDep }, deportistum);
        }

        // DELETE: api/Deportista/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeportistum(int id)
        {
            var deportistum = await _context.Deportista
                .Where(x => x.IdDep == id)
                .ToListAsync();
            if (deportistum == null)
            {
                return NotFound();
            }

            _context.Deportista.Remove(deportistum[0]);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DeportistumExists(int id)
        {
            return _context.Deportista.Any(e => e.IdDep == id);
        }
    }
}
