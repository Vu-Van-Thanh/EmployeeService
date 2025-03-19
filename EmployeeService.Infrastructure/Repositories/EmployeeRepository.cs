
using EmployeeService.Core.Domain.Entities;
using EmployeeService.Core.RepositoryContracts;

namespace EmployeeService.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public Task<Employee> GetEmployeeById(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}
