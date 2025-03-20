using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeService.Core.Domain.Entities;

namespace EmployeeService.Core.RepositoryContracts
{
    public interface IEmployeeMediaRepository
    {
        Task<List<EmployeeMedia>> GetAllAsync();
        Task<EmployeeMedia> GetByIdAsync(Guid id);
        Task AddAsync(EmployeeMedia employeeMedia);
        Task UpdateAsync(EmployeeMedia employeeMedia);
        Task DeleteAsync(Guid id);
        Task<Guid> GetEmployeeMediaIdByType(Guid employeeId, string Type);

    }
}
