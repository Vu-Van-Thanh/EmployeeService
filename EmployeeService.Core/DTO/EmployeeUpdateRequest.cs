using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.DTO
{
    public class EmployeeUpdateRequest
    {
        public string? Position { get; set; }   
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public string? Nationality { get; set; }
        public string? Ethnic { get; set; }
        public string? Religion { get; set; }
        public string? PlaceOfBirth { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? IndentityCard { get; set; }
        public string? PlaceIssued { get; set; }
        public string? Phone { get; set; }  // user 
        public string? Email { get; set; }  // user
        public string? Country { get; set; }
        public string? Province { get; set; }
        public string? District { get; set; }
        public string? Commune { get; set; }
        public string? Address { get; set; }  
        public string? InsuranceNumber { get; set; }
        public List<IdentityCard>? identityCardImage { get; set; }
        public List<InsuranceCard>? insuranceCardImage { get; set; }
        public IFormFile? avatar { get; set; }
    }
    public class IdentityCard
    {
        public IFormFile identityImage { get; set; }
        public string type { get; set; }
    }
    public class InsuranceCard
    {
        public IFormFile insuranceImage { get; set; }
        public string type { get; set; }
    }
}
