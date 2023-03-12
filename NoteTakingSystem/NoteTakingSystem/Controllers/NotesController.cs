using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoteTakingSystem.Models;

namespace NoteTakingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly NotesContext _context;

        public NotesController(NotesContext context)
        {
            _context = context;
        }

        // GET: api/Notes/id
        [HttpGet("usersnotes/{id}")]
        public async Task<ActionResult<IEnumerable<Notes>>> GetNotes(int id)
        {
            IEnumerable<Notes> notes = await _context.Notes.Where(s => s.UserId == id).ToListAsync();

            if(notes.Count() == 0)
            {
                return  NotFound();
            }
            return await _context.Notes.Where(s => s.UserId == id).ToListAsync();
        }

        // GET: api/Notes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Notes>> GetNote(int id)
        {
            var notes = await _context.Notes.FindAsync(id);

            if (notes == null)
            {
                return NotFound();
            }

            return notes;
        }

        // PUT: api/Notes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotes(int id, Notes notes)
        {
            if (id != notes.Id)
            {
                return BadRequest();
            }

            _context.Entry(notes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotesExists(id))
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

        // POST: api/Notes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Notes>> PostNotes(Notes notes)
        {
            _context.Notes.Add(notes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNotes", new { id = notes.Id }, notes);
        }

        // DELETE: api/Notes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotes(int id)
        {
            var notes = await _context.Notes.FindAsync(id);
            if (notes == null)
            {
                return NotFound();
            }

            _context.Notes.Remove(notes);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NotesExists(int id)
        {
            return _context.Notes.Any(e => e.Id == id);
        }
    }
}
