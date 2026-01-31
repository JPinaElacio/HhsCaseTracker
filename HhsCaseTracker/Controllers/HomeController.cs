using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HhsCaseTracker.Api.Data;
using HhsCaseTracker.Api.Models;
using Microsoft.Extensions.Logging;
using System.Reflection.Metadata.Ecma335;

namespace HhsCaseTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CaseController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CaseController> _logger;

        public CaseController(AppDbContext context, ILogger<CaseController> logger)
        {
            _context = context;
            _logger = logger; 
        }

        [HttpGet]
        public async Task<IActionResult> GetCases()
        {
            return Ok(await _context.Cases.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateCase([FromBody] Case newCase)
        {
            try
            {
                _context.Cases.Add(newCase);
                await _context.SaveChangesAsync();

                _logger.LogInformation("New case created with ID: {CaseId}, Department: {Department}",
                    newCase.CaseId, newCase.Department);


                return CreatedAtAction(nameof(GetCases), new { id = newCase.CaseId }, newCase);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating new case in department {Department}", newCase);
                return StatusCode(500, "An error occured while creating the case");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCase(int id, Case updateCase)
        {
            try
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

                _logger.LogInformation("Case updated with ID: {CaseId}, New Status: {Status}",
                    existingCase.CaseId, existingCase.Status);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating case with ID: {CaseId}", id);
                return StatusCode(500, "An error occured while updating the case");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCase(int id)
        {
            try
            {
                var existingCase = await _context.Cases.FindAsync(id);
                if (existingCase == null)
                    return NotFound();

                _context.Cases.Remove(existingCase);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Case deleted with ID: {CaseId}", existingCase.CaseId);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting case with ID: {CaseId}", id);
                return StatusCode(500, "An error occured while deleting the case");
            }
        }

    }
}
