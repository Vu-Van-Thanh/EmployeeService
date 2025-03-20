using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeService.Core.Domain.Entities;
using EmployeeService.Core.RepositoryContracts;
using EmployeeService.Infrastructure.AppDbContext;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService.Infrastructure.Repositories
{
    public class RelativeRepository : IRelativeRepository
    {
        private readonly ApplicationDbContext dbContext;

        public RelativeRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Relative>?> GetAllRelative()
        {
            return await dbContext.Relatives.ToListAsync();
        }

        public async Task<List<Relative>?> GetRelativeById(Guid employeeId)
        {
            return await dbContext.Relatives.Where(r => r.EmployeeID == employeeId).ToListAsync();
        }

        public async Task<Relative> UpsertRelative(Relative relative)
        {
            var existingRelative = await dbContext.Relatives
                .FirstOrDefaultAsync(r => r.RelativeID == relative.RelativeID);

            if (existingRelative == null)
            {
                await dbContext.Relatives.AddAsync(relative);
            }
            else
            {
                dbContext.Entry(existingRelative).CurrentValues.SetValues(relative);
            }

            await dbContext.SaveChangesAsync();
            return relative;
        }

    }
}
