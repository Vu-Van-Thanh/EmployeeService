using EmployeeService.Core.Domain.Entities;
using EmployeeService.Core.RepositoryContracts;
using EmployeeService.Infrastructure.AppDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Infrastructure.Repositories
{
    public class EvaluationPeriodRepository : IEvaluationPeriodRepository
    {
        private readonly ApplicationDbContext _context;

        public EvaluationPeriodRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> AddPeriod(EvaluationPeriod period)
        {
            try
            {
                _context.Set<EvaluationPeriod>().Add(period);
                await _context.SaveChangesAsync();
                return period.PeriodID;
            }
            catch (Exception ex)
            {
                // Log exception
                return Guid.Empty;
            }
        }

        public async Task<bool> DeletePeriod(Guid id)
        {
            var period = await _context.Set<EvaluationPeriod>().FindAsync(id);
            if (period == null)
                return false;

            _context.Set<EvaluationPeriod>().Remove(period);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<EvaluationPeriod>> GetActivePeriods()
        {
            DateTime now = DateTime.Now;
            return await _context.Set<EvaluationPeriod>()
                .Where(p => p.StartDate <= now && p.EndDate >= now)
                .ToListAsync();
        }

        public async Task<List<EvaluationPeriod>> GetAll()
        {
            return await _context.Set<EvaluationPeriod>().ToListAsync();
        }

        public async Task<EvaluationPeriod?> GetPeriodById(Guid id)
        {
            return await _context.Set<EvaluationPeriod>().FindAsync(id);
        }

        public async Task<List<EvaluationPeriod>> GetPeriodsByFilter(Expression<Func<EvaluationPeriod, bool>> filter)
        {
            return await _context.Set<EvaluationPeriod>()
                .Where(filter)
                .ToListAsync();
        }

        public async Task<Guid> UpdatePeriod(EvaluationPeriod period)
        {
            var existing = await _context.Set<EvaluationPeriod>().FindAsync(period.PeriodID);
            if (existing == null)
            {
                throw new Exception("Period not found");
            }

            existing.Name = period.Name;
            existing.StartDate = period.StartDate;
            existing.EndDate = period.EndDate;

            await _context.SaveChangesAsync();
            return existing.PeriodID;
        }
    }
} 