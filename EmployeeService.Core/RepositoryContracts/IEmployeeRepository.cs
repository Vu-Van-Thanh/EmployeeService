using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        Task<Guid> AddEmployee(Employee employee);
        Task<Employee?> GetEmployeeIdByUserId(Guid Id);
        Task<bool> DeleteEmployee(Guid employeeId);
        Task<List<Employee>> GetAllEmployeesByFeature(string feature, string value);
        Task<List<Employee>> GetEmployeesByFilter(Expression<Func<Employee, bool>> filter);
    }
}
