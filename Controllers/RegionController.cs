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
    public class RegionController : ControllerBase
    {
        private readonly RegistroPersonaContext _context;

        public RegionController(RegistroPersonaContext context)
        {
            _context = context;
        }

        // GET: api/Region
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Region>>> GetRegiones()
        {
            return await _context.Regions.ToListAsync();
        }

        // GET: api/Region/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Region>> GetRegion(int id)
        {
            var Region = await _context.Regions.FindAsync(id);

            if (Region == null)
            {
                return NotFound();
            }

            return Region;
        }

        // PUT: api/Region/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Region(int id, Region Region)
        {
            if (id != Region.Id)
            {
                return BadRequest();
            }

            _context.Entry(Region).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegionExists(id))
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

        // POST: api/Region
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Region>> Region(Region Region)
        {
            _context.Regions.Add(Region);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Region", new { id = Region.Id }, Region);
        }

        // DELETE: api/Region/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Region(int id)
        {
            var Region = await _context.Regions.FindAsync(id);
            if (Region == null)
            {
                return NotFound();
            }

            _context.Regions.Remove(Region);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RegionExists(int id)
        {
            return _context.Regions.Any(e => e.Id == id);
        }
    }
}
