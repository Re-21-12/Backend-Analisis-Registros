using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend_Analisis.Data;
using Backend_Analisis.Models;

namespace Backend_Analisis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly RegistroPersonaContext _context;

        public PersonaController(RegistroPersonaContext context)
        {
            _context = context;
        }

        // GET: api/Persona
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonaResponseDto>>> GetPersonas()
        {
            var personas = await _context.Personas
                .Include(p => p.Region)
                .Include(p => p.TipoPersona)
                .ToListAsync();

            return personas.Select(MapToResponseDto).ToList();
        }

        // GET: api/Persona/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonaResponseDto>> GetPersona(string id)
        {
            var persona = await _context.Personas
                .Include(p => p.Region)
                .Include(p => p.TipoPersona)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (persona == null)
            {
                return NotFound();
            }

            return MapToResponseDto(persona);
        }

        // PUT: api/Persona/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersona(string id, PersonaRequestDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var persona = await _context.Personas.FindAsync(id);
            if (persona == null)
            {
                return NotFound();
            }

            // Actualizar campos
            persona.PrimerNombre = dto.PrimerNombre;
            persona.SegundoNombre = dto.SegundoNombre;
            persona.PrimerApellido = dto.PrimerApellido;
            persona.SegundoApellido = dto.SegundoApellido;
            persona.FechaDeNacimiento = dto.FechaDeNacimiento;
            persona.FechaDeResidencia = dto.FechaDeResidencia;
            persona.TipoDeSangre = dto.TipoDeSangre;
            persona.RegionId = dto.RegionId;
            persona.TipoPersonaId = dto.TipoPersonaId;
            persona.Genero = dto.Genero;
            persona.Foto = dto.Foto;
            persona.Estado = dto.Estado;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/Persona
        [HttpPost]
        public async Task<ActionResult<Persona>> PostPersona(PersonaRequestDto dto)
        {
            var persona = new Persona
            {
                PrimerNombre = dto.PrimerNombre,
                SegundoNombre = dto.SegundoNombre,
                PrimerApellido = dto.PrimerApellido,
                SegundoApellido = dto.SegundoApellido,
                FechaDeNacimiento = dto.FechaDeNacimiento,
                FechaDeResidencia = dto.FechaDeResidencia,
                TipoDeSangre = dto.TipoDeSangre,
                RegionId = dto.RegionId,
                TipoPersonaId = dto.TipoPersonaId,
                Genero = dto.Genero,
                Foto = dto.Foto,
                Estado = dto.Estado
            };

            _context.Personas.Add(persona);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPersona), new { id = persona.Id }, persona);
        }

        // DELETE: api/Persona/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersona(string id)
        {
            var persona = await _context.Personas.FindAsync(id);
            if (persona == null)
            {
                return NotFound();
            }

            _context.Personas.Remove(persona);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonaExists(string id)
        {
            return _context.Personas.Any(e => e.Id == id);
        }
        private static PersonaResponseDto MapToResponseDto(Persona persona)
        {
            return new PersonaResponseDto
            {
                Id = persona.Id,
                PrimerNombre = persona.PrimerNombre,
                SegundoNombre = persona.SegundoNombre,
                PrimerApellido = persona.PrimerApellido,
                SegundoApellido = persona.SegundoApellido,
                FechaDeNacimiento = persona.FechaDeNacimiento,
                FechaDeResidencia = persona.FechaDeResidencia,
                TipoDeSangre = persona.TipoDeSangre,
                Genero = persona.Genero,
                Foto = persona.Foto,
                Estado = persona.Estado,
                RegionId = persona.Region?.Id,
                TipoPersonaId = persona.TipoPersona?.Id,
                RegionNombre = persona.Region?.Nombre, // Asumiendo que Region tiene "Nombre"
                TipoPersonaNombre = persona.TipoPersona?.Nombre // Igual aquí
            };
        }

    }
}
