
using EmployeeService.Core.Domain.Entities;
using EmployeeService.Core.RepositoryContracts;

namespace EmployeeService.Infrastructure.Repositories
{
    public class EmployeeMediaRepository : IEmployeeMediaRepository
    {
        public Task<EmployeeMedia> GetEmployeeMediaById(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}
