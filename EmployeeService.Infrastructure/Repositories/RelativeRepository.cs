using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<Guid> DeleteRelative(Relative relative)
        {
            await dbContext.Relatives
                .Where(r => r.RelativeID == relative.RelativeID)
                .ExecuteDeleteAsync();
            return relative.RelativeID;
        }

        public async Task<List<Relative>?> GetAllRelative()
        {
            return await dbContext.Relatives.ToListAsync();
        }

        public async Task<List<Relative>?> GetRelativeByFilter(Expression<Func<Relative, bool>> expression)
        {
            return await dbContext.Relatives
                .Where(expression)
                .ToListAsync();
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
                await dbContext.Relatives.AddAsync(relative); // Thêm mới
            }
            else
            {
                dbContext.Relatives.Update(relative); 
            }

            await dbContext.SaveChangesAsync();
            return relative;
        }


    }
}
