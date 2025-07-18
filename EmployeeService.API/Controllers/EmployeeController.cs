﻿using EmployeeService.Core.Domain.Entities;
using EmployeeService.Core.DTO;
using EmployeeService.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using EmployeeService.API.Kafka.Producer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeService.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IEventProducer _eventProducer;

        public EmployeeController(IEmployeeService employeeService, IEventProducer eventProducer)
        {
            _employeeService = employeeService;
            _eventProducer = eventProducer;
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

        [HttpGet("statistic")]
        public async Task<ActionResult<IEnumerable<EmployeeStatisticDTO>>> GetDataStatistic()
        {
            EmployeeStatisticDTO result = await  _employeeService.GetDataStatistic();
            if (result == null)
                return NotFound("No data found");
            return Ok(result);

        }

        [HttpGet("total-employee")]
        public async Task<ActionResult<IEnumerable<EmployeeTotal>>> GetEmployeeTotal()
        {
            EmployeeTotal result = await _employeeService.GetEmployeeTotal();
            if (result == null)
                return NotFound("No data found");
            return Ok(result);
        }

        [HttpGet("employee-department")]
        public async Task<ActionResult<IEnumerable<EmployeeDepartmentDTO>>> GetEmployeeDepartment()
        {
            var result= await _employeeService.GetEmployeeDepartment();
            


            return Ok(result);
        }

        [HttpGet("employeeIdList")]
        public async Task<ActionResult<IEnumerable<string>>> GetEmployeeIdList([FromQuery] EmployeeFilterDTO employeeFilter)
        {
            var employees = await _employeeService.GetEmployeeByFilter(employeeFilter);
            List<EmployeeDepartmentDTO> result = new List<EmployeeDepartmentDTO>();
            foreach (var employee in employees)
            {
                EmployeeDepartmentDTO employeeDepartmentDTO = new EmployeeDepartmentDTO();
                employeeDepartmentDTO.EmployeeID = employee.EmployeeID;
                employeeDepartmentDTO.EmployeeName = employee.FirstName + " " + employee.LastName;
                employeeDepartmentDTO.DepartmentID = employee.DepartmentID;
                employeeDepartmentDTO.DepartmentName = "";
                result.Add(employeeDepartmentDTO);
            }
            return Ok(result);
        }

        [HttpGet("filter-employee")]
        public async Task<ActionResult<IEnumerable<EmployeeInfo>>> GetEmployeeByFilter(EmployeeFilterDTO employeeFilter)
        {
           
            var employees = await _employeeService.GetEmployeeByFilter(employeeFilter);
            if (employees == null)
                return NotFound("No employees found with the given filter");

            return Ok(employees);
        }


        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<EmployeeInfo>>> GetEmployeeByFilter2([FromQuery] EmployeeFilterDTO employeeFilter)
        {

            var employees = await _employeeService.GetEmployeeByFilter(employeeFilter);
            if (employees == null)
                return NotFound("No employees found with the given filter");

            return Ok(employees);
        }

        /// <summary>
        /// Lấy thông tin chi tiết của một nhân viên
        /// </summary>
        [HttpGet("user/{id}")]
        public async Task<ActionResult<EmployeeInfo>> GetEmployeeById(Guid id)
        {
            var employee = await _employeeService.GetEmployeeById(id);
            if (employee == null)
                return NotFound("Employee not found");

            return Ok(employee);
        }

        [HttpGet("employeeName")]
        public async Task<ActionResult<IEnumerable<EmployeeNameInfo>>> GetEmployeeByName()
        {
            EmployeeFilterDTO employeeFilter = new EmployeeFilterDTO();
            List<EmployeeInfo> result1 = await _employeeService.GetEmployeeByFilter(employeeFilter);
            List<EmployeeNameInfo> result = new List<EmployeeNameInfo>();
            foreach (var employee in result1)
            {
                EmployeeNameInfo employeeNameInfo = new EmployeeNameInfo();
                employeeNameInfo.Name = employee.FirstName + " " + employee.LastName;
                employeeNameInfo.EmployeeId = employee.EmployeeID.ToString();
                result.Add(employeeNameInfo);
            }
            return Ok(result);
            
        }

        [HttpGet("profiles/{id}")]
        public async Task<ActionResult<EmployeeInfo>> GetUserProfile(string id)
        {
            var employee = await _employeeService.GetEmployeeIdByUserId(Guid.Parse(id));
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
            try
            {
                await _employeeService.AddEmployee(employee);
            }
            catch
            {
                return Ok("Không thể thêm người dùng");
            }
            return Ok("Thêm nhân viên thành công");
            
        }

        /// <summary>
        /// Cập nhật thông tin nhân viên
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee( [FromBody] EmployeeUpdateRequest employeeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            

            EmployeeUpdateResponse result = await _employeeService.UpdateEmployee(employeeDto);
           

            return Ok(result);
        }

        


        /// <summary>
        /// Xóa nhân viên
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            Guid employeeId = Guid.Parse((await _employeeService.GetEmployeeIdByUserId(id)).EmployeeID);
            bool result = await _employeeService.DeleteEmployee(id);
            if (!result)
                return NotFound("Employee not found");

            return Ok("Xóa thành công nhân viên");
        }


        [HttpPost("import-profile")]
        public async Task<IActionResult> ImportEmployee ( IFormFile formFile)
        {
            var memoryStream = new MemoryStream();
            await formFile.CopyToAsync(memoryStream);
            byte[] fileByte = memoryStream.ToArray();
            Guid employeeId = Guid.NewGuid();
            EmployeeImportDTO result = await _employeeService.ImportProfileFromExcelAsync(fileByte,employeeId.ToString());
            await _eventProducer.PublishAsync("employee-created", null, "employee-imported", result);
            return Ok(result);
        }
    }
}
