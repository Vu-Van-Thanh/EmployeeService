using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.Domain.Entities
{
    public class EmployeeContract
    {
        [Key]
        public Guid ContractId { get; set; } = Guid.NewGuid();

        
        public Guid? EmployeeId { get; set; }

        [StringLength(8)]
        public string ContractNumber { get; set; } = string.Empty; 
        public string ContractType { get; set; } = string.Empty;
        public DateTime StartDate { get; set; } 
        public DateTime? EndDate { get; set; } 
        public decimal SalaryIndex { get; set; } 
        public string Status { get; set; } = "Active";

        // Navigation Property
        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
    }
}
