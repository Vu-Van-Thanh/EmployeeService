using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeService.Core.DTO;

namespace EmployeeService.Core.Services
{
    public interface IEmployeeService
    {
        Task<EmployeeInfoResponse> GetEmployeeById(Guid Id);
        Task<EmployeeUpdateResponse> GetEmployeeById(EmployeeUpdateRequest employee);

    }
    public class EmployeeServices : IEmployeeService
    {
        public Task<EmployeeInfoResponse> GetEmployeeById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeUpdateResponse> GetEmployeeById(EmployeeUpdateRequest employee)
        {
            throw new NotImplementedException();
        }
    }
}
