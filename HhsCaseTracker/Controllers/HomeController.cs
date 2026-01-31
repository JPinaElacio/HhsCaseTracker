using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HhsCaseTracker.Api.Data;
using HhsCaseTracker.Api.Models;
using System.Reflection.Metadata.Ecma335;

namespace HhsCaseTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CaseController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CaseController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCases()
        {
            return Ok(await _context.Cases.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateCase([FromBody] Case newCase)
        {
            _context.Cases.Add(newCase);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCases), new { id = newCase.CaseId }, newCase);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCase(int id, Case updateCase)
        {
            if (id != updateCase.CaseId)
                return BadRequest("Case ID mismatch.");

            var existingCase = await _context.Cases.FindAsync(id);
            if (existingCase == null)
                return NotFound();

            existingCase.Title = updateCase.Title;
            existingCase.Description = updateCase.Description;
            existingCase.Department = updateCase.Department;
            existingCase.Status = updateCase.Status;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCase(int id)
        {
            var existingCase = await _context.Cases.FindAsync(id);
            if (existingCase == null)
                return NotFound();

            _context.Cases.Remove(existingCase);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
