using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EmployeeService.Core.Domain.Entities;

namespace EmployeeService.Core.DTO
{
    public class EducationFilterDTO
    {
        public string? Degree { get; set; }
        public string? EmployeeIDList { get; set; }
        public string? School { get; set; }
        public Expression<Func<Education, bool>> ToExpression()
        {
            List<Guid> employeeIds = string.IsNullOrEmpty(EmployeeIDList) ? new List<Guid>() : EmployeeIDList.Split(',').Select(id => Guid.Parse(id.Trim())).ToList();
            return education =>
                (string.IsNullOrEmpty(Degree) || education.Degree.ToString() == Degree) &&
                (string.IsNullOrEmpty(School) || education.School == School) &&
                (employeeIds.Count == 0 || employeeIds.Contains(education.EmployeeID));

        }

    }

}
