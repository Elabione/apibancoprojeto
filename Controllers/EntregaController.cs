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
    public class EntregaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EntregaController(AppDbContext context)
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

        // GET: api/Entrega
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Entrega>>> GetEntregas()
        {
          if (_context.Entregas == null)
          {
              return NotFound();
          }
            return await _context.Entregas.ToListAsync();
        }

        /// <summary>
        /// Retorna os entregas existentes
        /// </summary>
        /// <remarks>
        /// {
        ///    "idEnt": 0,
        ///    "dataValidade": "2024-04-25",
        ///    "dateEntrega": "2024-04-25",
        ///    "idEpi": 0,
        ///    "idCol": 0,
        ///    "idColNavigation": {
        ///      "idCol": 0,
        ///      "nomeCol": "string",
        ///      "ctps": "string",
        ///      "telefone": 0,
        ///      "cpf": 0,
        ///      "email": "string",
        ///      "dataAdmisão": "2024-04-25",
        ///  }
        /// </remarks>
        /// <response code="200">Sucesso no retorno dos dados</response>

        // GET: api/Entrega/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Entrega>> GetEntrega(int id)
        {
          if (_context.Entregas == null)
          {
              return NotFound();
          }
            var entrega = await _context.Entregas.FindAsync(id);

            if (entrega == null)
            {
                return NotFound();
            }

            return entrega;
        }

        /// <summary>
        /// Alteração  de dados da entrega
        /// </summary>
        ///  <remarks>
        /// {
        ///    "idEnt": 0,
        ///    "dataValidade": "2024-04-25",
        ///    "dateEntrega": "2024-04-25",
        ///    "idEpi": 0,
        ///    "idCol": 0,
        ///    "idColNavigation": {
        ///      "idCol": 0,
        ///      "nomeCol": "string",
        ///      "ctps": "string",
        ///      "telefone": 0,
        ///      "cpf": 0,
        ///      "email": "string",
        ///      "dataAdmisão": "2024-04-25",
        ///  }
        /// </remarks>
        /// <response code="200">Sucesso na atualização dos dados</response>

        // PUT: api/Entrega/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEntrega(int id, Entrega entrega)
        {
            if (id != entrega.IdEnt)
            {
                return BadRequest();
            }

            _context.Entry(entrega).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntregaExists(id))
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
        /// Inserir  uma nova entrega
        /// </summary>
        ///  <remarks>
        /// {
        ///    "idEnt": 0,
        ///    "dataValidade": "2024-04-25",
        ///    "dateEntrega": "2024-04-25",
        ///    "idEpi": 0,
        ///    "idCol": 0,
        ///    "idColNavigation": {
        ///      "idCol": 0,
        ///      "nomeCol": "string",
        ///      "ctps": "string",
        ///      "telefone": 0,
        ///      "cpf": 0,
        ///      "email": "string",
        ///      "dataAdmisão": "2024-04-25",
        ///  }
        /// </remarks>
        /// <response code="200">Sucesso no upload dos dados</response>

        // POST: api/Entrega
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Entrega>> PostEntrega(Entrega entrega)
        {
          if (_context.Entregas == null)
          {
              return Problem("Entity set 'AppDbContext.Entregas'  is null.");
          }
            _context.Entregas.Add(entrega);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EntregaExists(entrega.IdEnt))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEntrega", new { id = entrega.IdEnt }, entrega);
        }

        /// <summary>
        /// Deletar  um registro de Entrega por Id
        /// </summary>

        // DELETE: api/Entrega/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntrega(int id)
        {
            if (_context.Entregas == null)
            {
                return NotFound();
            }
            var entrega = await _context.Entregas.FindAsync(id);
            if (entrega == null)
            {
                return NotFound();
            }

            _context.Entregas.Remove(entrega);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EntregaExists(int id)
        {
            return (_context.Entregas?.Any(e => e.IdEnt == id)).GetValueOrDefault();
        }
    }
}
