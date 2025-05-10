using EmployeeService.Core.Domain.Entities;
using EmployeeService.Core.DTO;
using EmployeeService.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeeContractController : ControllerBase
    {
        private readonly IEmployeeContractService _contractService;

        public EmployeeContractController(IEmployeeContractService contractService)
        {
            _contractService = contractService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllContracts()
        {
            var contracts = await _contractService.GetAllContracts();
            return Ok(contracts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContractById(Guid id)
        {
            var contract = await _contractService.GetContractById(id);
            if (contract == null)
                return NotFound();
            return Ok(contract);
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<IActionResult> GetContractsByEmployeeId(Guid employeeId)
        {
            var contracts = await _contractService.GetContractsByEmployeeId(employeeId);
            return Ok(contracts);
        }

        [HttpPost]
        public async Task<IActionResult> AddContract([FromForm] EmployeeContractAddRequest contract)
        {
            await _contractService.UploadContractFileAsync(contract);
            return Ok("Thêm hợp đồng thành công");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContract(Guid id, [FromBody] EmployeeContractAddRequest contract)
        {
            if (id != contract.ContractId)
                return BadRequest();

            await _contractService.UpsertContractAsync(contract);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContract(Guid id)
        {
            await _contractService.DeleteContractAsync(id);
            return NoContent();
        }

        
    }
}
