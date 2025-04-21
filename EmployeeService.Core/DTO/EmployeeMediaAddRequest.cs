using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EmployeeService.Core.DTO
{
    public class EmployeeMediaAddRequest
    {
        public Guid EmployeeId { get; set; }
        public string MediaType { get; set; }   // Avartar/IdentityCard/InsuranceCard
        public string MediaUrl { get; set; }

        public IFormFile[]? Images { get; set; }
    }
}
