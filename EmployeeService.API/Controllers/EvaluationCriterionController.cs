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
    public class EvaluationCriterionController : ControllerBase
    {
        private readonly IEvaluationCriterionService _criterionService;

        public EvaluationCriterionController(IEvaluationCriterionService criterionService)
        {
            _criterionService = criterionService;
        }

        /// <summary>
        /// Get all evaluation criteria
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EvaluationCriterionDTO>>> GetAllCriteria()
        {
            var criteria = await _criterionService.GetAllCriteria();
            return Ok(criteria);
        }

        /// <summary>
        /// Get criterion by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<EvaluationCriterionDTO>> GetCriterionById(Guid id)
        {
            var criterion = await _criterionService.GetCriterionById(id);
            if (criterion == null)
                return NotFound("Criterion not found");

            return Ok(criterion);
        }

        /// <summary>
        /// Get criteria by category
        /// </summary>
        [HttpGet("category/{category}")]
        public async Task<ActionResult<IEnumerable<EvaluationCriterionDTO>>> GetCriterionsByCategory(string category)
        {
            var criteria = await _criterionService.GetCriterionsByCategory(category);
            return Ok(criteria);
        }

        /// <summary>
        /// Filter criteria by parameters
        /// </summary>
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<EvaluationCriterionDTO>>> GetCriterionsByFilter([FromQuery] EvaluationCriterionFilterDTO filter)
        {
            var criteria = await _criterionService.GetCriterionsByFilter(filter);
            return Ok(criteria);
        }

        /// <summary>
        /// Add a new criterion
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> CreateCriterion([FromBody] EvaluationCriterionAddDTO criterion)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var id = await _criterionService.AddCriterion(criterion);
                if (id == Guid.Empty)
                    return BadRequest("Failed to add criterion");

                return Ok(new { id, message = "Criterion added successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Update an existing criterion
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCriterion(Guid id, [FromBody] EvaluationCriterionDTO criterion)
        {
            if (id != criterion.CriterionID)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _criterionService.UpdateCriterion(criterion);
                if (result == Guid.Empty)
                    return NotFound("Criterion not found or update failed");

                return Ok("Criterion updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Delete a criterion
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCriterion(Guid id)
        {
            try
            {
                bool result = await _criterionService.DeleteCriterion(id);
                if (result)
                {
                    return Ok("Criterion deleted successfully");
                }
                else
                {
                    return NotFound("Criterion not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
} 