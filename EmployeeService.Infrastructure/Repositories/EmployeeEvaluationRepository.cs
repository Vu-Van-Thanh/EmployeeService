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
    public class EmployeeEvaluationRepository : IEmployeeEvaluationRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeEvaluationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> AddEvaluation(EmployeeEvaluation evaluation)
        {
            try
            {
                _context.EmployeeEvaluations.Add(evaluation);
                await _context.SaveChangesAsync();
                return evaluation.ID;
            }
            catch (Exception ex)
            {
                // Log exception
                return Guid.Empty;
            }
        }

        public async Task<bool> DeleteEvaluation(Guid id)
        {
            var evaluation = await _context.EmployeeEvaluations.FindAsync(id);
            if (evaluation == null)
                return false;

            _context.EmployeeEvaluations.Remove(evaluation);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<EmployeeEvaluation>> GetAll()
        {
            return await _context.EmployeeEvaluations
                .Include(e => e.Employee)
                .Include(e => e.Period)
                .ToListAsync();
        }

        public async Task<EmployeeEvaluation?> GetEvaluationById(Guid id)
        {
            return await _context.EmployeeEvaluations
                .Include(e => e.Employee)
                .Include(e => e.Period)
                .FirstOrDefaultAsync(e => e.ID == id);
        }

        public async Task<List<EmployeeEvaluation>> GetEvaluationsByCriterior(Guid employeeId)
        {
            return await _context.EmployeeEvaluations
                                .Include(e => e.Employee)
                                .Include(e => e.Period)
                                .Where(e => e.EvaluatorId == employeeId)
                                .ToListAsync();
        }

        public async Task<List<EmployeeEvaluation>> GetEvaluationsByEmployeeId(Guid employeeId)
        {
            return await _context.EmployeeEvaluations
                .Include(e => e.Employee)
                .Include(e => e.Period)
                .Where(e => e.EmployeeID == employeeId)
                .ToListAsync();
        }

        public async Task<List<EmployeeEvaluation>> GetEvaluationsByFilter(Expression<Func<EmployeeEvaluation, bool>> filter)
        {
            return await _context.EmployeeEvaluations
                .Include(e => e.Employee)
                .Include(e => e.Period)
                .Where(filter)
                .ToListAsync();
        }

        public async Task<List<EmployeeEvaluation>> GetEvaluationsByPeriodId(Guid periodId)
        {
            return await _context.EmployeeEvaluations
                .Include(e => e.Employee)
                .Include(e => e.Period)
                .Where(e => e.PeriodId == periodId)
                .ToListAsync();
        }

        public async Task<Guid> UpdateEvaluation(EmployeeEvaluation evaluation)
        {
            var existing = await _context.EmployeeEvaluations.FindAsync(evaluation.ID);
            if (existing == null)
            {
                throw new Exception("Evaluation not found");
            }

            existing.EmployeeID = evaluation.EmployeeID;
            existing.EvaluatorId = evaluation.EvaluatorId;
            existing.PeriodId = evaluation.PeriodId;
            existing.EvaluationDate = evaluation.EvaluationDate;
            existing.TotalScore = evaluation.TotalScore;
            existing.DetailJson = evaluation.DetailJson;
            existing.DetailJsonManager = evaluation.DetailJsonManager;
            existing.Status = evaluation.Status;

            await _context.SaveChangesAsync();
            return existing.ID;
        }
    }
} 