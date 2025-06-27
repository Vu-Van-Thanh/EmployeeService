using EmployeeService.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.RepositoryContracts
{
    public interface IEmployeeEvaluationRepository
    {
        Task<EmployeeEvaluation?> GetEvaluationById(Guid id);
        Task<Guid> AddEvaluation(EmployeeEvaluation evaluation);
        Task<Guid> UpdateEvaluation(EmployeeEvaluation evaluation);
        Task<bool> DeleteEvaluation(Guid id);
        Task<List<EmployeeEvaluation>> GetAll();
        Task<List<EmployeeEvaluation>> GetEvaluationsByEmployeeId(Guid employeeId);
        Task<List<EmployeeEvaluation>> GetEvaluationsByCriterior(Guid employeeId);
        Task<List<EmployeeEvaluation>> GetEvaluationsByPeriodId(Guid periodId);
        Task<List<EmployeeEvaluation>> GetEvaluationsByFilter(Expression<Func<EmployeeEvaluation, bool>> filter);
    }
} 