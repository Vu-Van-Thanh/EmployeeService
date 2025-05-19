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
    public class EvaluationCriterionRepository : IEvaluationCriterionRepository
    {
        private readonly ApplicationDbContext _context;

        public EvaluationCriterionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> AddCriterion(EvaluationCriterion criterion)
        {
            try
            {
                _context.EvaluationCriterias.Add(criterion);
                await _context.SaveChangesAsync();
                return criterion.CriterionID;
            }
            catch (Exception ex)
            {
                // Log exception
                return Guid.Empty;
            }
        }

        public async Task<bool> DeleteCriterion(Guid id)
        {
            var criterion = await _context.EvaluationCriterias.FindAsync(id);
            if (criterion == null)
                return false;

            _context.EvaluationCriterias.Remove(criterion);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<EvaluationCriterion>> GetAll()
        {
            return await _context.EvaluationCriterias.ToListAsync();
        }

        public async Task<EvaluationCriterion?> GetCriterionById(Guid id)
        {
            return await _context.EvaluationCriterias.FindAsync(id);
        }

        public async Task<List<EvaluationCriterion>> GetCriterionsByCategory(string category)
        {
            return await _context.EvaluationCriterias
                .Where(c => c.Category == category)
                .ToListAsync();
        }

        public async Task<List<EvaluationCriterion>> GetCriterionsByFilter(Expression<Func<EvaluationCriterion, bool>> filter)
        {
            return await _context.EvaluationCriterias
                .Where(filter)
                .ToListAsync();
        }

        public async Task<Guid> UpdateCriterion(EvaluationCriterion criterion)
        {
            var existing = await _context.EvaluationCriterias.FindAsync(criterion.CriterionID);
            if (existing == null)
            {
                throw new Exception("Criterion not found");
            }

            existing.Name = criterion.Name;
            existing.Description = criterion.Description;
            existing.Weight = criterion.Weight;
            existing.Category = criterion.Category;

            await _context.SaveChangesAsync();
            return existing.CriterionID;
        }
    }
} 