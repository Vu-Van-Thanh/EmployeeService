using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeService.Core.Domain.Entities;

namespace EmployeeService.Core.RepositoryContracts
{
    public interface IEmployeeRepository
    {
        public Task<Employee?> GetEmployeeById(Guid Id);
        public Task<Guid> UpdateEmployee(Employee employee);
        Task<List<Employee>> GetAll();
        Task<bool> AddEmployee(Employee employee);
        Task<Employee?> GetEmployeeIdByUserId(Guid Id);
        Task<bool> DeleteEmployee(Guid employeeId);
        Task<List<Employee>> GetAllEmployeesByFeature(string feature, string value);
    }
}
