using EmployeeService.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.DTO
{
    public class EducationDTO
    {
        public Guid EducationID { get; set; }
        public Guid EmployeeID { get; set; }
        public string? Degree { get; set; }
        public string? Major { get; set; }
        public string? School { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Description { get; set; }
    }

    public static class EducationDTOExtensions
    {
        public static EducationDTO ToDTO(this Education education)
        {
            return new EducationDTO
            {
                EducationID = education.EducationID,
                EmployeeID = education.EmployeeID,
                Degree = education.Degree.ToString(),
                Major = education.Major,
                School = education.School,
                StartDate = education.StartDate,
                EndDate = education.EndDate,
                Description = education.Description
            };
        }
    }
}
