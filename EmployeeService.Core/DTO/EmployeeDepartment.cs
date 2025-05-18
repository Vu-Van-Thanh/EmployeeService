using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.DTO
{
    public class EmployeeDepartment
    {
        public List<string> DepartmentID { get; set; }
        public List<string> EmployeeIDList { get; set; }
    }
}
