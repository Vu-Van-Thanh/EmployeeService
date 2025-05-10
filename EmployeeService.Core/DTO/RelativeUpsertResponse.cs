using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeService.Core.Domain.Entities;

namespace EmployeeService.Core.DTO
{
    public class RelativeUpsertResponse
    {
        public Guid RelativeID { get; set; }
        public Guid? EmployeeID { get; set; }
        public string? FullName { get; set; }
        public string? RelativeType { get; set; }
    }
    public static class RelativeExtension
    {
        public static RelativeUpsertResponse ToRelativeUpsertResponse(this Relative relative)
        {
            return new RelativeUpsertResponse
            {
                RelativeID = relative.RelativeID,
                EmployeeID = relative.EmployeeID,
                FullName = $"{relative.FirstName} {relative.LastName}",
                RelativeType = relative.RelativeType
            };
        }

        public static RelativeInfoResponse ToRelativeInfoResponse(this Relative relative)
        {
            return new RelativeInfoResponse
            {
                EmployeeID = relative.EmployeeID,
                FirstName = relative.FirstName,
                RelativeType = relative.RelativeType,
                LastName = relative.LastName,
                DateOfBirth = relative.DateOfBirth,
                Address = relative.Address,
                Nationality = relative.Nationality,
                Ethnic = relative.Ethnic,
                Religion = relative.Religion,
                PlaceOfBirth = relative.PlaceOfBirth,
                IndentityCard = relative.IndentityCard,
                Country = relative.Country,
                Province = relative.Province,
                District = relative.District,
                Commune = relative.Commune,
                PhoneNumber = relative.PhoneNumber
            };
        }
    }

}
