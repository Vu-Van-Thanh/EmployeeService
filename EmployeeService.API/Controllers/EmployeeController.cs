using EmployeeService.Core.DTO;
using EmployeeService.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// Lấy danh sách tất cả nhân viên
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeInfo>>> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllEmployees();
            return Ok(employees);
        }

        /// <summary>
        /// Lấy thông tin chi tiết của một nhân viên
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeInfo>> GetEmployeeById(Guid id)
        {
            var employee = await _employeeService.GetEmployeeById(id);
            if (employee == null)
                return NotFound("Employee not found");

            return Ok(employee);
        }

        /// <summary>
        /// Thêm mới một nhân viên
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> CreateEmployee([FromBody] EmployeeAddRequest employee)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(!(await _employeeService.AddEmployee(employee)))
            {
                return Ok("Không thể thêm người dùng");
            };
            return Ok("Thêm nhân viên thành công");
            
        }

        /// <summary>
        /// Cập nhật thông tin nhân viên
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(Guid userId, [FromBody] EmployeeUpdateRequest employeeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Guid employeeId = await _employeeService.GetEmployeeIdByUserId(userId);

            EmployeeUpdateResponse result = await _employeeService.UpdateEmployee(employeeDto, employeeId);
           

            return Ok(result);
        }

        /// <summary>
        /// Xóa nhân viên
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            Guid employeeId = await _employeeService.GetEmployeeIdByUserId(id);
            bool result = await _employeeService.DeleteEmployee(id);
            if (!result)
                return NotFound("Employee not found");

            return Ok("Xóa thành công nhân viên");
        }
    }
}
