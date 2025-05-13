using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.DTO
{
    public class EmployeeByDepartmentAndGender
    {
        public string departmentName { get; set; }
        public int male {  get; set; }
        public int female { get; set; }

    }
}
