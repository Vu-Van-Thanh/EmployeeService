using EmployeeService.Core.DTO;
using EmployeeService.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RelativeController : ControllerBase
    {
        private readonly IRelativeService _relativeService;
        private readonly IEmployeeService _employeeService;

        public RelativeController(IRelativeService relativeService, IEmployeeService employeeService)
        {
            _relativeService = relativeService;
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRelative()
        {
            return Ok(await _relativeService.GetAllRelative());
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetRelativeByEmployeeId(Guid id)
        {
            return Ok(await _relativeService.GetRelativeByEmployee(id));
        }


        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateRelative(Guid userId, [FromBody] RelativeUpsertRequest relative)
        {
            relative.EmployeeID = Guid.Parse((await _employeeService.GetEmployeeIdByUserId(userId)).EmployeeID);
            return Ok((await _relativeService.relativeUpsertResponse(relative)));
        }


    }
}
