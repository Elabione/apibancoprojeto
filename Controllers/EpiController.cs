using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apiprojeto.Context;
using apiprojeto.Models;

namespace apiprojeto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EpiController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna os Colaboradores existente
        /// </summary>
        /// <remarks>
        /// {
        ///    "idCol": 0,
        ///    "nomeCol": "string",
        ///    "ctps": "string",
        ///    "telefone": 0,
        ///    "cpf": 0,
        ///    "email": "string",
        ///    "dataAdmisão": "2024-04-25",
        ///    "entregas": []
        ///  }
        /// </remarks>
        /// <response code="200">Sucesso no retorno dos dados</response>

        // GET: api/Epi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Epi>>> GetEpis()
        {
          if (_context.Epis == null)
          {
              return NotFound();
          }
            return await _context.Epis.ToListAsync();
        }

        /// <summary>
        /// Retorna  uma EPI por id
        /// </summary>
        /// <remarks>
        /// {
        ///    "idEpi": 0,
        ///    "insUso": "string",
        ///    "nomeEpi": "string",
        ///    "qtd": 0,
        ///  }
        /// </remarks>
        /// <response code="200">Sucesso no retorno dos dados</response>

        // GET: api/Epi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Epi>> GetEpi(int id)
        {
          if (_context.Epis == null)
          {
              return NotFound();
          }
            var epi = await _context.Epis.FindAsync(id);

            if (epi == null)
            {
                return NotFound();
            }

            return epi;
        }

        /// <summary>
        /// Alteração  de dados da EPI
        /// </summary>
        ///  <remarks>
        /// {
        ///    "idEpi": 0,
        ///    "insUso": "string",
        ///    "nomeEpi": "string",
        ///    "qtd": 0,
        ///  }
        /// </remarks>
        /// <response code="200">Sucesso na atualização dos dados</response>

        // PUT: api/Epi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEpi(int id, Epi epi)
        {
            if (id != epi.IdEpi)
            {
                return BadRequest();
            }

            _context.Entry(epi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EpiExists(id))
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

        /// <summary>
        /// Inserir  novo registro na tabela EPI
        /// </summary>
        ///  <remarks>
        /// {
        ///    "idEpi": 0,
        ///    "insUso": "string",
        ///    "nomeEpi": "string",
        ///    "qtd": 0,
        ///  }
        /// </remarks>
        /// <response code="200">Sucesso no upload dos dados</response>

        // POST: api/Epi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Epi>> PostEpi(Epi epi)
        {
          if (_context.Epis == null)
          {
              return Problem("Entity set 'AppDbContext.Epis'  is null.");
          }
            _context.Epis.Add(epi);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EpiExists(epi.IdEpi))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEpi", new { id = epi.IdEpi }, epi);
        }

        /// <summary>
        /// Deletar  um registro da tabela EPI
        /// </summary>

        // DELETE: api/Epi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEpi(int id)
        {
            if (_context.Epis == null)
            {
                return NotFound();
            }
            var epi = await _context.Epis.FindAsync(id);
            if (epi == null)
            {
                return NotFound();
            }

            _context.Epis.Remove(epi);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EpiExists(int id)
        {
            return (_context.Epis?.Any(e => e.IdEpi == id)).GetValueOrDefault();
        }
    }
}
