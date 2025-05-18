using EmployeeService.Core.DTO;
using EmployeeService.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeService.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EvaluationPeriodController : ControllerBase
    {
        private readonly IEvaluationPeriodService _periodService;

        public EvaluationPeriodController(IEvaluationPeriodService periodService)
        {
            _periodService = periodService;
        }

        /// <summary>
        /// Get all evaluation periods
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EvaluationPeriodDTO>>> GetAllPeriods()
        {
            var periods = await _periodService.GetAllPeriods();
            return Ok(periods);
        }

        /// <summary>
        /// Get period by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<EvaluationPeriodDTO>> GetPeriodById(Guid id)
        {
            var period = await _periodService.GetPeriodById(id);
            if (period == null)
                return NotFound("Period not found");

            return Ok(period);
        }

        /// <summary>
        /// Get active periods (overlapping with current date)
        /// </summary>
        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<EvaluationPeriodDTO>>> GetActivePeriods()
        {
            var periods = await _periodService.GetActivePeriods();
            return Ok(periods);
        }

        /// <summary>
        /// Filter periods by parameters
        /// </summary>
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<EvaluationPeriodDTO>>> GetPeriodsByFilter([FromQuery] EvaluationPeriodFilterDTO filter)
        {
            var periods = await _periodService.GetPeriodsByFilter(filter);
            return Ok(periods);
        }

        /// <summary>
        /// Add a new period
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> CreatePeriod([FromBody] EvaluationPeriodAddDTO period)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var id = await _periodService.AddPeriod(period);
                if (id == Guid.Empty)
                    return BadRequest("Failed to add period");

                return Ok(new { id, message = "Period added successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Update an existing period
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePeriod(Guid id, [FromBody] EvaluationPeriodDTO period)
        {
            if (id != period.PeriodID)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _periodService.UpdatePeriod(period);
                if (result == Guid.Empty)
                    return NotFound("Period not found or update failed");

                return Ok("Period updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Delete a period
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePeriod(Guid id)
        {
            try
            {
                bool result = await _periodService.DeletePeriod(id);
                if (result)
                {
                    return Ok("Period deleted successfully");
                }
                else
                {
                    return NotFound("Period not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
} 