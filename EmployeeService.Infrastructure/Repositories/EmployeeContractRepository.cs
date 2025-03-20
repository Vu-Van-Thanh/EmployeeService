using EmployeeService.Core.Domain.Entities;
using EmployeeService.Core.RepositoryContracts;
using EmployeeService.Infrastructure.AppDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Infrastructure.Repositories
{
    public class EmployeeContractRepository : IEmployeeContractRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EmployeeContractRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<EmployeeContract>> GetAllContractsAsync()
        {
            return await _dbContext.EmployeeContracts.ToListAsync();
        }

        public async Task<EmployeeContract?> GetContractByIdAsync(Guid id)
        {
            return await _dbContext.EmployeeContracts.FirstOrDefaultAsync(c => c.ContractId == id);
        }

        public async Task<List<EmployeeContract>> GetContractsByEmployeeIdAsync(Guid employeeId)
        {
            return await _dbContext.EmployeeContracts
                .Where(c => c.EmployeeId == employeeId)
                .ToListAsync();
        }

        public async Task AddContractAsync(EmployeeContract contract)
        {
            await _dbContext.EmployeeContracts.AddAsync(contract);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<EmployeeContract> UpsertContractAsync(EmployeeContract contract)
        {
            var existingContract = await _dbContext.EmployeeContracts
                .AsNoTracking() 
                .FirstOrDefaultAsync(c => c.ContractId == contract.ContractId);

            if (existingContract == null)
            {
                await _dbContext.EmployeeContracts.AddAsync(contract);
            }
            else
            {
                _dbContext.EmployeeContracts.Update(contract); 
            }

            await _dbContext.SaveChangesAsync();
            return contract;
        }

        public async Task DeleteContractAsync(Guid id)
        {
            var contract = await _dbContext.EmployeeContracts.FirstOrDefaultAsync(c => c.ContractId == id);
            if (contract != null)
            {
                _dbContext.EmployeeContracts.Remove(contract);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
