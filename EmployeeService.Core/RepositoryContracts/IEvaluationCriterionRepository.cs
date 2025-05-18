using EmployeeService.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.RepositoryContracts
{
    public interface IEvaluationCriterionRepository
    {
        Task<EvaluationCriterion?> GetCriterionById(Guid id);
        Task<Guid> AddCriterion(EvaluationCriterion criterion);
        Task<Guid> UpdateCriterion(EvaluationCriterion criterion);
        Task<bool> DeleteCriterion(Guid id);
        Task<List<EvaluationCriterion>> GetAll();
        Task<List<EvaluationCriterion>> GetCriterionsByCategory(string category);
        Task<List<EvaluationCriterion>> GetCriterionsByFilter(Expression<Func<EvaluationCriterion, bool>> filter);
    }
} 