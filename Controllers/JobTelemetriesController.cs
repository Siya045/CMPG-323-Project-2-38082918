using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _38082918.Models;

namespace _38082918.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobTelemetriesController : ControllerBase
    {
        private readonly TechTrends2Context _context;

        public JobTelemetriesController(TechTrends2Context context)
        {
            _context = context;
        }

        // GET: api/JobTelemetries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobTelemetry>>> GetJobTelemetries()
        {
            return await _context.JobTelemetries.ToListAsync();
        }

        // GET: api/JobTelemetries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobTelemetry>> GetJobTelemetry(int id)
        {
            var jobTelemetry = await _context.JobTelemetries.FindAsync(id);

            if (jobTelemetry == null)
            {
                return NotFound();
            }

            return jobTelemetry;
        }

        // PUT: api/JobTelemetries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobTelemetry(int id, JobTelemetry jobTelemetry)
        {
            if (id != jobTelemetry.Id)
            {
                return BadRequest();
            }

            _context.Entry(jobTelemetry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobTelemetryExists(id))
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

        // POST: api/JobTelemetries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JobTelemetry>> PostJobTelemetry(JobTelemetry jobTelemetry)
        {
            _context.JobTelemetries.Add(jobTelemetry);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJobTelemetry", new { id = jobTelemetry.Id }, jobTelemetry);
        }

        // DELETE: api/JobTelemetries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobTelemetry(int id)
        {
            var jobTelemetry = await _context.JobTelemetries.FindAsync(id);
            if (jobTelemetry == null)
            {
                return NotFound();
            }

            _context.JobTelemetries.Remove(jobTelemetry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Private method to check if a JobTelemetry exists
        private bool JobTelemetryExists(int id)
        {
            return _context.JobTelemetries.Any(e => e.Id == id);
        }

        // GET: api/JobTelemetries/GetSavings?projectId=1&startDate=2024-01-01&endDate=2024-12-31
        [HttpGet("GetSavings")]
        public async Task<ActionResult<SavingsResult>> GetSavings(Guid projectId, DateTime startDate, DateTime endDate)
        {
            // Query the telemetry data based on Project ID and Date range
            var telemetryData = await _context.JobTelemetries
                .Where(t => t.ProjectID == projectId && t.EntryDate >= startDate && t.EntryDate <= endDate)
                .ToListAsync();

            if (telemetryData == null || telemetryData.Count == 0)
            {
                return NotFound("No telemetry data found for the given parameters.");
            }

            // Calculate cumulative time and cost savings
            var totalTimeSaved = telemetryData.Sum(t => t.HumanTime ?? 0);
            var totalCostSaved = telemetryData.Sum(t => CalculateCostSaving(t.HumanTime));

            // Return the result as a custom object
            var result = new SavingsResult
            {
                TotalTimeSaved = totalTimeSaved,
                TotalCostSaved = totalCostSaved
            };

            return Ok(result);
        }

        // GET: api/JobTelemetries/GetSavingsByClient?clientId=1&startDate=2024-01-01&endDate=2024-12-31
        [HttpGet("GetSavingsByClient")]
        public async Task<ActionResult<SavingsResult>> GetSavingsByClient(Guid clientId, DateTime startDate, DateTime endDate)
        {
            // Query the telemetry data based on Client ID and Date range
            var telemetryData = await _context.JobTelemetries
                .Where(t => t.ClientID == clientId && t.EntryDate >= startDate && t.EntryDate <= endDate)
                .ToListAsync();

            if (telemetryData == null || telemetryData.Count == 0)
            {
                return NotFound("No telemetry data found for the given parameters.");
            }

            // Calculate cumulative time and cost savings
            var totalTimeSaved = telemetryData.Sum(t => t.HumanTime ?? 0);
            var totalCostSaved = telemetryData.Sum(t => CalculateCostSaving(t.HumanTime));

            // Return the result as a custom object
            var result = new SavingsResult
            {
                TotalTimeSaved = totalTimeSaved,
                TotalCostSaved = totalCostSaved
            };

            return Ok(result);
        }

        // Private method to calculate cost savings based on time saved
        private decimal CalculateCostSaving(int? humanTime)
        {
            // Example calculation (e.g., $50/hour rate)
            const decimal hourlyRate = 50m;
            return (humanTime ?? 0) * (hourlyRate / 60); // Assuming humanTime is in minutes
        }
    }
    // Custom class to represent the result
    public class SavingsResult
    {
        public int TotalTimeSaved { get; set; }
        public decimal TotalCostSaved { get; set; }
    }
}

