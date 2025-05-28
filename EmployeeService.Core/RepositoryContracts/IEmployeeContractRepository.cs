using EmployeeService.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.RepositoryContracts
{
    public interface IEmployeeContractRepository
    {
        Task<List<EmployeeContract>> GetAllContractsAsync();
        Task<EmployeeContract?> GetContractByIdAsync(Guid id);
        Task<List<EmployeeContract>> GetContractsByEmployeeIdAsync(Guid employeeId);
        Task AddContractAsync(EmployeeContract contract);
        Task<EmployeeContract> UpsertContractAsync(EmployeeContract contract);
        Task DeleteContractAsync(Guid id);
        Task<EmployeeContract?> GetContractByFilter(Expression<Func<EmployeeContract, bool>> expression);
    }
}
