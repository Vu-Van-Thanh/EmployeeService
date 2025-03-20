
using EmployeeService.Core.Domain.Entities;
using EmployeeService.Core.RepositoryContracts;
using EmployeeService.Infrastructure.AppDbContext;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService.Infrastructure.Repositories
{
    public class EmployeeMediaRepository : IEmployeeMediaRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeMediaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmployeeMedia>> GetAllAsync()
        {
            return  await _context.EmployeeMedias.ToListAsync();
        }

        public async Task<EmployeeMedia> GetByIdAsync(Guid id)
        {
            return await _context.EmployeeMedias.FindAsync(id);
        }

        public async Task AddAsync(EmployeeMedia employeeMedia)
        {
            await _context.EmployeeMedias.AddAsync(employeeMedia);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(EmployeeMedia employeeMedia)
        {
            _context.EmployeeMedias.Update(employeeMedia);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.EmployeeMedias.FindAsync(id);
            if (entity != null)
            {
                _context.EmployeeMedias.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Guid> GetEmployeeMediaIdByType(Guid employeeId, string Type)
        {
            return  await _context.EmployeeMedias
                .Where(e => e.EmployeeID == employeeId && e.MediaType == Type)
                .Select(e => e.EmployeeMediaID)
                .FirstOrDefaultAsync();
        }
    }
}
