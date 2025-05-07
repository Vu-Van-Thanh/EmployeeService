using EmployeeService.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.DTO
{
    public class EmployeeFilterDTO
    {
        public string? Department { get; set; }
        public string? JobTitle { get; set; }
        public string? ManagerId { get; set; }
        public string? EmployeeId { get; set; }

        public Expression<Func<Employee, bool>> ToExpression()
        {
            List<Guid> employeeIds = string.IsNullOrEmpty(EmployeeId)
              ? new List<Guid>()
              : EmployeeId.Split(',').Select(id => Guid.Parse(id.Trim())).ToList();
            Guid? managerId = string.IsNullOrEmpty(ManagerId) ? null : Guid.Parse(ManagerId);

            return employee =>
                (string.IsNullOrEmpty(Department) || employee.DepartmentID == Department) &&
                (string.IsNullOrEmpty(JobTitle) || employee.Position == JobTitle) &&
                (!managerId.HasValue || employee.ManagerID == managerId.Value) &&
                 (employeeIds.Count == 0 || employeeIds.Contains(employee.EmployeeID));
        }
    }
}
