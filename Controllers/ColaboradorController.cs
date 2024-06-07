using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apiprojeto.Context;
using apiprojeto.Models;
using Microsoft.AspNetCore.Authorization;

namespace apiprojeto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColaboradorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ColaboradorController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna os Colaboradores existente
        /// </summary>
        /// <remarks>
        /// {
        ///    "idCol": 0,
        ///    "nomeCol": "int",
        ///    "ctps": "int",
        ///    "telefone": 0,
        ///    "cpf": 0,
        ///    "email": "string",
        ///    "dataAdmisão": "2024-04-25",
        ///    "entregas": []
        ///  }
        /// </remarks>
        /// <response code="200">Sucesso no retorno dos dados</response>

        // GET: api/Colaborador
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Colaborador>>> GetColaboradors()
        {
          if (_context.Colaboradors == null)
          {
              return NotFound();
          }
            return await _context.Colaboradors.ToListAsync();
        }

        /// <summary>
        /// Retorna os Colaboradores existente
        /// </summary>
        /// <remarks>
        /// {
        ///    "idCol": 0,
        ///    "nomeCol": "string",
        ///    "ctps": "int",
        ///    "telefone": 0,
        ///    "cpf": 0,
        ///    "email": "string",
        ///    "dataAdmisão": "2024-04-25",
        ///    "entregas": []
        ///  }
        /// </remarks>
        /// <response code="200">Sucesso no retorno dos dados</response>

        // GET: api/Colaborador/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Colaborador>> GetColaborador(int id)
        {
          if (_context.Colaboradors == null)
          {
              return NotFound();
          }
            var colaborador = await _context.Colaboradors.FindAsync(id);

            if (colaborador == null)
            {
                return NotFound();
            }

            return colaborador;
        }

        /// <summary>
        /// Alteração conforme dados passados para o id informado
        /// </summary>
        /// <remarks>
        ///  {
        ///    "idCol": 0,
        ///    "nomeCol": "string",
        ///    "ctps": "int",
        ///    "telefone": 0,
        ///    "cpf": 0,
        ///    "email": "string",
        ///    "dataAdmisão": "2024-04-25"
        ///  }
        /// </remarks>
        /// <response code="200">Sucesso na atualização dos dados</response>

        // PUT: api/Colaborador/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize("Admin")]
        public async Task<IActionResult> PutColaborador(int id, Colaborador colaborador)
        {
            if (id != colaborador.IdCol)
            {
                return BadRequest();
            }

            _context.Entry(colaborador).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColaboradorExists(id))
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
        /// Inserir  um novo registro de colaborador no banco de dados
        /// </summary>
        /// <remarks>
        ///  {
        ///    "idCol": 0,
        ///    "nomeCol": "string",
        ///    "ctps": "int",
        ///    "telefone": 0,
        ///    "cpf": 0,
        ///    "email": "string",
        ///    "dataAdmisão": "2024-04-25"
        ///  }
        /// </remarks>
        /// <response code="200">Sucesso no upload dos dados</response>

        // POST: api/Colaborador
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize("Admin")]
        public async Task<ActionResult<Colaborador>> PostColaborador(Colaborador colaborador)
        {
          if (_context.Colaboradors == null)
          {
              return Problem("Entity set 'AppDbContext.Colaboradors'  is null.");
          }
            _context.Colaboradors.Add(colaborador);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ColaboradorExists(colaborador.IdCol))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetColaborador", new { id = colaborador.IdCol }, colaborador);
        }

        /// <summary>
        /// deletar  um registro de colaborador por Id 
        /// </summary>

        // DELETE: api/Colaborador/5
        [HttpDelete("{id}")]
        [Authorize("Admin")]
        public async Task<IActionResult> DeleteColaborador(int id)
        {
            if (_context.Colaboradors == null)
            {
                return NotFound();
            }
            var colaborador = await _context.Colaboradors.FindAsync(id);
            if (colaborador == null)
            {
                return NotFound();
            }

            _context.Colaboradors.Remove(colaborador);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ColaboradorExists(int id)
        {
            return (_context.Colaboradors?.Any(e => e.IdCol == id)).GetValueOrDefault();
        }
        [HttpGet("Epis")]
        [Authorize]
        public async Task <ActionResult<IEnumerable<Entrega>>> GetEpiCollab (int id){
            if (_context.Entregas == null){
                return NotFound();
            } else {
                var epi = await _context.Entregas.Where(e=>e.IdCol == id).ToListAsync();
                if (epi == null){
                    return NotFound();

                } else{
                    return epi;
                }
            }
        }
    }
}
