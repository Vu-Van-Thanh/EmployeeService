using EmployeeService.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.DTO
{
    public class EmployeeContractInfo
    {
        public Guid? EmployeeId { get; set; }
        public string ContractNumber { get; set; } 
        public string ContractType { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal SalaryIndex { get; set; }
        public string Status { get; set; }
    }

    public static class ContractExtension
    {
        public static EmployeeContractInfo ToEmployeeContractInfo(this EmployeeContract contract)
        {
            return new EmployeeContractInfo
            {
                EmployeeId = contract.EmployeeId,
                ContractNumber = contract.ContractNumber,
                ContractType = contract.ContractType,
                StartDate = contract.StartDate,
                EndDate = contract.EndDate,
                SalaryIndex = contract.SalaryIndex,
                Status = contract.Status
            };
        }
    }

}
