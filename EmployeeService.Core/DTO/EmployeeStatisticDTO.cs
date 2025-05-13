using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.DTO
{
    public class EmployeeStatisticDTO
    {
        public List<EmployeeByDepartment>? employeeDepartment { get; set; }
        public EmployeeByGender? employeeGender {get;set;}
        public List<EmployeeByDepartmentAndGender>? employeeByDepartmentAndGender { get; set; }
        public List<EmployeeByDegree>? employeeByDegree { get; set; }
        public List<EmployeeByRegion>? employeeByRegion { get; set; }

    }
}
