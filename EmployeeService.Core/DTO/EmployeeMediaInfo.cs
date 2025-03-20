using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.DTO
{
    public class EmployeeMediaInfo
    {
        public Guid EmployeeId { get; set; }
        public string MediaUrl { get; set; }
        public string MediaType { get; set; }
    }
    
}
