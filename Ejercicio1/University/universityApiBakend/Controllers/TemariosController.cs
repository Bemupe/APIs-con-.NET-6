using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using universityApiBakend.DataAccess;
using universityApiBakend.Models.DataModels;

namespace universityApiBakend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemariosController : ControllerBase
    {
        private readonly UniversityDBContext _context;

        public TemariosController(UniversityDBContext context)
        {
            _context = context;
        }

        // GET: api/Temarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Temario>>> GetTemarios()
        {
            return await _context.Temarios.ToListAsync();
        }

        // GET: api/Temarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Temario>> GetTemario(int id)
        {
            var temario = await _context.Temarios.FindAsync(id);

            if (temario == null)
            {
                return NotFound();
            }

            return temario;
        }

        // PUT: api/Temarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTemario(int id, Temario temario)
        {
            if (id != temario.Id)
            {
                return BadRequest();
            }

            _context.Entry(temario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TemarioExists(id))
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

        // POST: api/Temarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Temario>> PostTemario(Temario temario)
        {
            _context.Temarios.Add(temario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTemario", new { id = temario.Id }, temario);
        }

        // DELETE: api/Temarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTemario(int id)
        {
            var temario = await _context.Temarios.FindAsync(id);
            if (temario == null)
            {
                return NotFound();
            }

            _context.Temarios.Remove(temario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TemarioExists(int id)
        {
            return _context.Temarios.Any(e => e.Id == id);
        }
    }
}
