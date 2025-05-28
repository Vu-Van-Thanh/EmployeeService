using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.DTO
{
    public class RelativeUpsertRequest
    {
        public Guid? RelativeID { get; set; }
        public Guid? EmployeeID { get; set; }
        public string? FirstName { get; set; }
        public string? RelativeType { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Nationality { get; set; } = "Việt Nam";
        public string? Ethnic { get; set; }
        public string? Religion { get; set; } = "Không";
        public string? PlaceOfBirth { get; set; }
        public string? IndentityCard { get; set; }
        public string? Country { get; set; }
        public string? Province { get; set; }
        public string? District { get; set; }
        public string? Commune { get; set; }
        public string? OldID {get;set;}
        
    }
}
