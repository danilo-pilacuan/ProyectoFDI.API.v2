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
    public class ResultadoBloquesController : ControllerBase
    {
        private readonly ProyectoFdiV2Context _context;

        public ResultadoBloquesController(ProyectoFdiV2Context context)
        {
            _context = context;
        }

        // GET: api/ResultadoBloques
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResultadoBloque>>> GetResultadoBloques()
        {
          if (_context.ResultadoBloques == null)
          {
              return NotFound();
          }
            return await _context.ResultadoBloques.ToListAsync();
        }

        // GET: api/ResultadoBloques/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResultadoBloque>> GetResultadoBloque(int id)
        {
          if (_context.ResultadoBloques == null)
          {
              return NotFound();
          }
            var resultadoBloque = await _context.ResultadoBloques.FindAsync(id);

            if (resultadoBloque == null)
            {
                return NotFound();
            }

            return resultadoBloque;
        }



        [HttpGet("{idcom}/{iddep}/{etapa}")]
        public async Task<ActionResult<ResultadoBloque>> GetPuntajeBloque(int idcom, int iddep, string etapa)
        {
            var puntajeBloque = await _context.ResultadoBloques
                .Where(p => p.IdCom == idcom && p.IdDep == iddep && p.Etapa == etapa)
                .SingleOrDefaultAsync();

            if (puntajeBloque == null)
            {
                return NotFound();
            }

            return puntajeBloque;
        }



        // PUT: api/ResultadoBloques/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResultadoBloque(int id, ResultadoBloque resultadoBloque)
        {
            if (id != resultadoBloque.IdResBloque)
            {
                return BadRequest();
            }

            _context.Entry(resultadoBloque).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResultadoBloqueExists(id))
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

        // POST: api/ResultadoBloques
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ResultadoBloque>> PostResultadoBloque(ResultadoBloque resultadoBloque)
        {
          if (_context.ResultadoBloques == null)
          {
              return Problem("Entity set 'ProyectoFdiV2Context.ResultadoBloques'  is null.");
          }
            _context.ResultadoBloques.Add(resultadoBloque);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetResultadoBloque", new { id = resultadoBloque.IdResBloque }, resultadoBloque);
        }

        // DELETE: api/ResultadoBloques/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResultadoBloque(int id)
        {
            if (_context.ResultadoBloques == null)
            {
                return NotFound();
            }
            var resultadoBloque = await _context.ResultadoBloques.FindAsync(id);
            if (resultadoBloque == null)
            {
                return NotFound();
            }

            _context.ResultadoBloques.Remove(resultadoBloque);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ResultadoBloqueExists(int id)
        {
            return (_context.ResultadoBloques?.Any(e => e.IdResBloque == id)).GetValueOrDefault();
        }
    }
}
