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
    public class VistaPuntajesDeportistasController : ControllerBase
    {
        private readonly ProyectoFdiV2Context _context;

        public VistaPuntajesDeportistasController(ProyectoFdiV2Context context)
        {
            _context = context;
        }

        // GET: api/VistaPuntajesDeportistas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VistaPuntajesDeportista>>> GetVistaPuntajesDeportista()
        {
          if (_context.VistaPuntajesDeportistas == null)
          {
              return NotFound();
          }
            return await _context.VistaPuntajesDeportistas.ToListAsync();
        }

        // GET: api/VistaPuntajesDeportistas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VistaPuntajesDeportista>> GetVistaPuntajesDeportista(long id)
        {
          if (_context.VistaPuntajesDeportistas == null)
          {
              return NotFound();
          }
            var vistaPuntajesDeportista = await _context.VistaPuntajesDeportistas.FindAsync(id);

            if (vistaPuntajesDeportista == null)
            {
                return NotFound();
            }

            return vistaPuntajesDeportista;
        }

        [HttpGet("ByCompetencia/{idCompetencia}/{etapa}")]
        public async Task<ActionResult<IEnumerable<VistaPuntajesDeportista>>> GetVistaPuntajesDeportistaByCompetencia(int idCompetencia, string etapa)
        {
            var vistaPuntajesDeportista = await _context.VistaPuntajesDeportistas
                                                        .Where(vp => vp.IdCom == idCompetencia && vp.Etapa == etapa)
                                                        .ToListAsync();

            if (vistaPuntajesDeportista == null || vistaPuntajesDeportista.Count == 0)
            {
                return NotFound();
            }

            return vistaPuntajesDeportista;
        }


    }
}
