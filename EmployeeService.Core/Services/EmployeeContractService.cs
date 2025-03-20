using EmployeeService.Core.Domain.Entities;
using EmployeeService.Core.DTO;
using EmployeeService.Core.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.Services
{
    public interface IEmployeeContractService
    {
        Task<List<EmployeeContractInfo>> GetAllContracts();
        Task<EmployeeContractInfo?> GetContractById(Guid id);
        Task<List<EmployeeContractInfo>> GetContractsByEmployeeId(Guid employeeId);
        Task<EmployeeContractInfo> UpsertContractAsync(EmployeeContractAddRequest contract);
        Task DeleteContractAsync(Guid id);
    }
    public class EmployeeContractService : IEmployeeContractService
    {
        private readonly IEmployeeContractRepository _contractRepository;

        public EmployeeContractService(IEmployeeContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }

        public async Task<List<EmployeeContractInfo>> GetAllContracts()
        {
            List<EmployeeContract> contracts =  await _contractRepository.GetAllContractsAsync();
            return contracts.Select(c => c.ToEmployeeContractInfo()).ToList();
        }

        public async Task<EmployeeContractInfo?> GetContractById(Guid id)
        {
            return (await _contractRepository.GetContractByIdAsync(id)).ToEmployeeContractInfo();
        }

        public async Task<List<EmployeeContractInfo>> GetContractsByEmployeeId(Guid employeeId)
        {
            List<EmployeeContract> contracts = await _contractRepository.GetContractsByEmployeeIdAsync(employeeId);
            return contracts.Select(c => c.ToEmployeeContractInfo()).ToList();
        }


        public async Task<EmployeeContractInfo> UpsertContractAsync(EmployeeContractAddRequest contract)
        {
           
            EmployeeContract employeeContract = new EmployeeContract
            {
                ContractId = contract.ContractId ?? Guid.NewGuid(),
                EmployeeId = contract.EmployeeId,
                ContractType = contract.ContractType,
                ContractNumber = contract.ContractNumber,
                StartDate = contract.StartDate,
                EndDate = contract.EndDate,
                SalaryIndex = contract.SalaryIndex,
                Status = contract.Status

            };
            return (await _contractRepository.UpsertContractAsync(employeeContract)).ToEmployeeContractInfo();
        }

        public async Task DeleteContractAsync(Guid id)
        {
            await _contractRepository.DeleteContractAsync(id);
        }

       
    }

}
