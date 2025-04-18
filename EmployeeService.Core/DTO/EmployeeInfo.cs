using EmployeeService.Core.Domain.Entities;
using EmployeeService.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.DTO
{
    
    public class EmployeeInfo
    {
        public string EmployeeID { get; set; } 
        public string? Position { get; set; }
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? Gender { get; set; }

        public string Tax { get; set; }
        public string? Address { get; set; }

        public string? Nationality { get; set; } = "Việt Nam";

        public string? Ethnic { get; set; }

        public string? Religion { get; set; } = "Không";

        public string? PlaceOfBirth { get; set; }

        public string? IndentityCard { get; set; }
        public string? PlaceIssued { get; set; }

        public string? Country { get; set; }

        public string? Province { get; set; }

        public string? District { get; set; }

        public string? Commune { get; set; }
        public string? InsuranceNumber { get; set; }
    }

    public static class EmployeeExtension
    {
        public static EmployeeInfo ToEmployeeInfo(this Employee employee)
        {
            return new EmployeeInfo
            {
                EmployeeID = employee.EmployeeID.ToString(),
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                DateOfBirth = employee.DateOfBirth,
                Gender = employee.Gender == GenderOptions.Male ? "Nam" : "Nữ",
                Tax = employee.Tax,
                Address = employee.Address,
                Nationality = employee.Nationality,
                Ethnic = employee.Ethnic,
                Religion = employee.Religion,
                PlaceOfBirth = employee.PlaceOfBirth,
                IndentityCard = employee.IndentityCard,
                PlaceIssued = employee.PlaceIssued,
                Country = employee.Country,
                Province = employee.Province,
                District = employee.District,
                Commune = employee.Commune,
                InsuranceNumber = employee.InsuranceNumber,
                Position = employee.Position
            };
        }
    }

}
