using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeService.Core.Domain.Entities;

namespace EmployeeService.Core.DTO
{
    public class EmployeeMediaAddResponse
    {
        public string EmployeeId { get; set; }
        public string MediaUrl { get; set; }
        public string MediaType { get; set; }
    }
    public static class EmployeeMediaExtension
    {
        
        public static EmployeeMediaAddResponse ToEmployeeMediaAddResponse(this EmployeeMedia employeeMedia)
        {
            return new EmployeeMediaAddResponse
            {
                EmployeeId = employeeMedia.EmployeeID.ToString(),
                MediaUrl = employeeMedia.MediaUrl,
                MediaType = employeeMedia.MediaType
            };
        }

        public static EmployeeMediaInfo ToEmployeeMediaInfo(this EmployeeMedia employeeMedia)
        {
            return new EmployeeMediaInfo
            {
                EmployeeId = employeeMedia.EmployeeID,
                MediaUrl = employeeMedia.MediaUrl,
                MediaType = employeeMedia.MediaType
            };
        }
    }
}
