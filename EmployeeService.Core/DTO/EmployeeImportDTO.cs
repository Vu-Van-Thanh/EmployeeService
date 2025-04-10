using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeService.Core.Domain.Entities;
using EmployeeService.Core.Enums;

namespace EmployeeService.Core.DTO
{
    public class EmployeeImportDTO
    {
        public string? path { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth  { get; set; }
        public GenderOptions? Gender { get; set; }
        public string? Tax { get; set; }
        public string? BankAccountOwner { get; set; }
        public string? BankAccountName{ get; set; }
        public string? Vehicle { get; set; }
        public string? PlaceOfBirth { get; set; }
        public string? PlaceIssued { get; set; }


        public string? Nationality { get; set; } = "Việt Nam";
        public string? Ethnic { get; set; }
        public string? Religion { get; set; } = "Không";

        public string? IndentityCard { get; set; }

        public string? Country { get; set; }
        public string? Province { get; set; }
        public string? District { get; set; }
        public string? Commune { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Phone   { get; set; }

        public string? InsuranceNumber { get; set; }
        public List<RelativeDTO>? Relatives { get; set; } = new();
        public List<ExperienceDTO>? experiences { get; set; } = new();
    }

    public class RelativeDTO
    {
        public string Relationship { get; set; }
        public string FullName { get; set; }
        public string Job { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

    }

    public class ExperienceDTO
    {
        public string ProjectName { get; set; }
        public string Company { get; set; }
        public string Position { get; set; }
        public string PL { get; set; }
        public string Description { get; set; }

    }
}
