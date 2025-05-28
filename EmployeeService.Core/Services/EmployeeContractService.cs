using AutoMapper.Configuration.Annotations;
using EmployeeService.Core.Domain.Entities;
using EmployeeService.Core.DTO;
using EmployeeService.Core.RepositoryContracts;
using System;
using System.Collections;
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
        Task<bool> UploadContractFileAsync(EmployeeContractAddRequest request);
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

        public async  Task<bool> UploadContractFileAsync(EmployeeContractAddRequest request)
        {
            EmployeeContract? exist = null;
            if (!String.IsNullOrEmpty(request.OldContractNumber))
            {
                exist  = await _contractRepository.GetContractByFilter(c => c.ContractNumber == request.OldContractNumber && c.EmployeeId == request.EmployeeId);
            }
            if (request.contractFile == null || request.contractFile.Length == 0)
                return false ;
            EmployeeContract employeeContract;
            try
            {
                // Đường dẫn thư mục lưu file
                string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "EmployeeContracts");

                // Tạo thư mục nếu chưa tồn tại
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                // Tạo tên file duy nhất
                Guid contractId = request.ContractId ?? Guid.NewGuid();
                string fileName = $"{contractId}_{request.EmployeeId}.docx";
                string filePath = Path.Combine(uploadFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.contractFile.CopyToAsync(stream);
                }
                if(exist == null)
                {
                    employeeContract = new EmployeeContract
                    {
                        ContractId = exist.ContractId,
                        EmployeeId = request.EmployeeId,
                        ContractType = request.ContractType,
                        ContractNumber = request.ContractNumber ?? "",
                        StartDate = request.StartDate,
                        EndDate = request.EndDate,
                        ContractUrl = $"/Uploads/EmployeeContracts/{fileName}",
                        Position = request.Position,
                        SalaryBase = request.SalaryBase,
                        SalaryIndex = request.SalaryIndex,
                        Status = request.Status
                    };
                }
                else
                {
                    employeeContract = new EmployeeContract
                    {
                        ContractId = contractId,
                        EmployeeId = request.EmployeeId,
                        ContractType = request.ContractType,
                        ContractNumber = request.ContractNumber ?? "",
                        StartDate = request.StartDate,
                        EndDate = request.EndDate,
                        ContractUrl = $"/Uploads/EmployeeContracts/{fileName}",
                        Position = request.Position,
                        SalaryBase = request.SalaryBase,
                        SalaryIndex = request.SalaryIndex,
                        Status = request.Status
                    };

                }
                

                await _contractRepository.UpsertContractAsync(employeeContract);

                return true;
            }
            catch
            {
                return false;
            }

        }
    }

}
