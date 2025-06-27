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
    public class EmployeeEvaluationController : ControllerBase
    {
        private readonly IEmployeeEvaluationService _evaluationService;

        public EmployeeEvaluationController(IEmployeeEvaluationService evaluationService)
        {
            _evaluationService = evaluationService;
        }

        /// <summary>
        /// Get all employee evaluations
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeEvaluationDTO>>> GetAllEvaluations()
        {
            var evaluations = await _evaluationService.GetAllEvaluations();
            return Ok(evaluations);
        }

        /// <summary>
        /// Get evaluation by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeEvaluationDTO>> GetEvaluationById(Guid id)
        {
            var evaluation = await _evaluationService.GetEvaluationById(id);
            if (evaluation == null)
                return NotFound("Evaluation not found");

            return Ok(evaluation);
        }

        /// <summary>
        /// Get evaluations by employee ID
        /// </summary>
        [HttpGet("employee/{employeeId}")]
        public async Task<ActionResult<IEnumerable<EmployeeEvaluationDTO>>> GetEvaluationsByEmployeeId(Guid employeeId)
        {
            var evaluations = await _evaluationService.GetEvaluationsByEmployeeId(employeeId);
            return Ok(evaluations);
        }

        [HttpGet("personal/{employeeId}")]
        public async Task<ActionResult<IEnumerable<EncryptEvaluationDTO>>> GetEvaluationsRelateEmployeeId(Guid employeeId)
        {
            var evaluations = await _evaluationService.GetEncryptEvaluationsByEmployee(employeeId);
            return Ok(evaluations);
        }

        [HttpGet("criterior/{employeeId}")]
        public async Task<ActionResult<IEnumerable<EncryptEvaluationDTO>>> GetEvaluationsRelateBycriterior(Guid employeeId)
        {
            var evaluations = await _evaluationService.GetEncryptEvaluationsByCriterior(employeeId);
            return Ok(evaluations);
        }

        /// <summary>
        /// Get evaluations by period ID
        /// </summary>
        [HttpGet("period/{periodId}")]
        public async Task<ActionResult<IEnumerable<EmployeeEvaluationDTO>>> GetEvaluationsByPeriodId(Guid periodId)
        {
            var evaluations = await _evaluationService.GetEvaluationsByPeriodId(periodId);
            return Ok(evaluations);
        }

        /// <summary>
        /// Filter evaluations by criteria
        /// </summary>
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<EmployeeEvaluationDTO>>> GetEvaluationsByFilter([FromQuery] EmployeeEvaluationFilterDTO filter)
        {
            var evaluations = await _evaluationService.GetEvaluationsByFilter(filter);
            return Ok(evaluations);
        }

        /// <summary>
        /// Add a new evaluation
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> CreateEvaluation([FromBody] EmployeeEvaluationAddDTO evaluation)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var id = await _evaluationService.AddEvaluation(evaluation);
                if (id == Guid.Empty)
                    return BadRequest("Failed to add evaluation");

                return Ok(new { id, message = "Evaluation added successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Update an existing evaluation
        /// </summary>
        [HttpPut()]
        public async Task<ActionResult> UpdateEvaluation( [FromBody] EmployeeEvaluationDTO evaluation)
        {
          

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _evaluationService.UpdateEvaluation(evaluation);
                if (result == Guid.Empty)
                    return NotFound("Evaluation not found or update failed");

                return Ok(new { message = "Evaluation updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Delete an evaluation
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvaluation(Guid id)
        {
            try
            {
                bool result = await _evaluationService.DeleteEvaluation(id);
                if (result)
                {
                    return Ok("Evaluation deleted successfully");
                }
                else
                {
                    return NotFound("Evaluation not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Get statistics for all evaluation criteria
        /// </summary>
        [HttpGet("statistics")]
        public async Task<ActionResult<IEnumerable<CriterionStatisticDTO>>> GetCriterionStatistics()
        {
            try
            {
                var statistics = await _evaluationService.GetCriterionStatistics();
                return Ok(statistics);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
} 