﻿using EmployeeService.Core.DTO;
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


        [HttpPost]
        public async Task<IActionResult> UpdateRelative([FromBody] RelativeUpsertRequest relative)
        {
            
            return Ok((await _relativeService.relativeUpsertResponse(relative)));
        }

        [HttpDelete("{CCCD}")]
        public async Task<IActionResult> DeleteRelative(string CCCD)
        {
            var relative = await _relativeService.DeleteRelative(CCCD);
            if (relative == Guid.Empty)
            {
                return NotFound(new { message = "Relative not found" });
            }
            return Ok(new { message = "Relative deleted successfully" });
        }

    }
}
