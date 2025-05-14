using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.DTO
{
    public class EmployeeTotal
    {
        public List<int> employeeGrowthByMonth { get; set; }
        public List<int> newEmployeesByMonth { get; set; }
        public List<int> leaveEmployeesByMonth { get; set; }
        public List<double> retentionRate { get; set; }
        public List<double> GrowthRate { get; set; }
    }
}
