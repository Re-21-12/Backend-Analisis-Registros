using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend_Analisis.Data;
using Backend_Analisis.Models;

namespace Backend_Analisis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoPersonaController : ControllerBase
    {
        private readonly RegistroPersonaContext _context;

        public TipoPersonaController(RegistroPersonaContext context)
        {
            _context = context;
        }

        // GET: api/TipoPersona
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoPersona>>> GetTipoTipoPersonas()
        {
            return await _context.TipoPersonas.ToListAsync();
        }

        // GET: api/TipoPersona/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoPersona>> GetTipoPersona(int id)
        {
            var TipoPersona = await _context.TipoPersonas.FindAsync(id);

            if (TipoPersona == null)
            {
                return NotFound();
            }

            return TipoPersona;
        }

        // PUT: api/TipoPersona/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoPersona(int id, TipoPersona TipoPersona)
        {
            if (id != TipoPersona.Id)
            {
                return BadRequest();
            }

            _context.Entry(TipoPersona).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoPersonaExists(id))
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

        // POST: api/TipoPersona
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TipoPersona>> PostTipoPersona(TipoPersona TipoPersona)
        {
            _context.TipoPersonas.Add(TipoPersona);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoPersona", new { id = TipoPersona.Id }, TipoPersona);
        }

        // DELETE: api/TipoPersona/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoPersona(int id)
        {
            var TipoPersona = await _context.TipoPersonas.FindAsync(id);
            if (TipoPersona == null)
            {
                return NotFound();
            }

            _context.TipoPersonas.Remove(TipoPersona);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipoPersonaExists(int id)
        {
            return _context.TipoPersonas.Any(e => e.Id == id);
        }
    }
}
