using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.DTO
{
    public class EmployeeContractAddRequest
    {
        public Guid? ContractId { get; set; }
        public Guid EmployeeId { get; set; }
        public string? ContractNumber { get; set; }
        public string ContractType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal SalaryIndex { get; set; }

        public decimal SalaryBase { get; set; }
        public string ? OldContractNumber { get; set; }
        public string Position { get;set; }
        public IFormFile contractFile { get;set; }
        public string Status { get; set; }
    }
}
