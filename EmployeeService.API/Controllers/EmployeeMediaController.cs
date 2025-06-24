using EmployeeService.Core.Domain.Entities;
using EmployeeService.Core.DTO;
using EmployeeService.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeeMediaController : ControllerBase
    {
        private readonly IEmployeeMediaService _service;
        private readonly IEmployeeService _employeeService;

        public EmployeeMediaController(IEmployeeMediaService service, IEmployeeService employeeService)
        {
            _service = service;
            _employeeService = employeeService; 
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> CreateMedia([FromForm] EmployeeMediaAddRequest employeeMedia, Guid id)
        {
            if(employeeMedia.EmployeeId == Guid.Empty)
            {
               employeeMedia.EmployeeId = Guid.Parse((await _employeeService.GetEmployeeIdByUserId(id)).EmployeeID);
            }
            try
            {
                List<EmployeeMediaAddResponse> createdMedia = await _service.AddAsync(employeeMedia);
                return Ok(createdMedia);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

           
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, EmployeeMediaUpdateRequest employeeMedia)
        {
            if (employeeMedia.EmployeeId == Guid.Empty)
            {
                employeeMedia.EmployeeId = Guid.Parse((await _employeeService.GetEmployeeIdByUserId(id)).EmployeeID);
            }
            await _service.UpdateAsync(employeeMedia);
            return Ok("Updated");
        }

        [HttpPost("UpdateMedia")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateMedia( [FromForm] EmployeeMediaUpdateRequest employeeMedia)
        {
            
            await _service.UpdateAsync(employeeMedia);
            return Ok("Updated");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
