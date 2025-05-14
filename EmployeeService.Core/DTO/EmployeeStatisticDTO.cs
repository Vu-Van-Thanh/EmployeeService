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
        public List<SeniorityEmployee> seniorityEmployees { get; set; } = new List<SeniorityEmployee>
        {
            new SeniorityEmployee { type = "Trên 10 năm", count = 0 },
            new SeniorityEmployee { type = "5-10 năm", count = 0 },
            new SeniorityEmployee { type = "3-5 năm", count = 0 },
            new SeniorityEmployee { type = "1-3 năm", count = 0 },
            new SeniorityEmployee { type = "Dưới 1 năm", count = 0 }
        };


    }
}
