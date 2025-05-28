using EmployeeService.Core.Domain.Entities;
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
    public class EducationController : ControllerBase
    {
        private readonly IEducationService _educationService;
        private readonly IEventProducer _eventProducer;

        public EducationController(IEducationService educationService, IEventProducer eventProducer)
        {
            _educationService = educationService;
            _eventProducer = eventProducer;
        }

        /// <summary>
        /// Lấy danh sách tất cả nhân viên
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EducationDTO>>> GetAllEducation()
        {
            var employees = await _educationService.GetAllEducations();
            return Ok(employees);
        }

       /* [HttpGet("statistic")]
        public async Task<ActionResult<IEnumerable<EmployeeByDepartment>>> GetDataStatistic()
        {
            
            
        }*/

        [HttpGet("filter-education")]
        public async Task<ActionResult<IEnumerable<EducationDTO>>> GetEducationByFilter([FromQuery] EducationFilterDTO educationFilter)
        {
           
            var employees = await _educationService.GetEducationsByFilter(educationFilter);
            if (employees == null)
                return NotFound("No employees found with the given filter");

            return Ok(employees);
        }

        

      

        /// <summary>
        /// Thêm mới một nhân viên
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> CreateEducation([FromBody] EducationAddDTO education)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                await _educationService.AddEducation(education);
            }
            catch
            {
                return Ok("Không thể thêm người dùng");
            }
            return Ok("Thêm nhân viên thành công");
            
        }

        

        /// <summary>
        /// Xóa nhân viên
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEducation(Guid id)
        {
            try
            {
                bool result = await _educationService.DeleteEducation(id);
                if (result)
                {
                    return Ok("Xóa nhân viên thành công");
                }
                else
                {
                    return NotFound("Không tìm thấy nhân viên để xóa");
                }
            }
            catch
            {
                return Ok("Xóa nhân viên không thành công");
            }


        }



    }
}
