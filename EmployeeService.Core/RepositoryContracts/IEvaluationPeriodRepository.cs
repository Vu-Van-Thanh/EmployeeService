using EmployeeService.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.RepositoryContracts
{
    public interface IEvaluationPeriodRepository
    {
        Task<EvaluationPeriod?> GetPeriodById(Guid id);
        Task<Guid> AddPeriod(EvaluationPeriod period);
        Task<Guid> UpdatePeriod(EvaluationPeriod period);
        Task<bool> DeletePeriod(Guid id);
        Task<List<EvaluationPeriod>> GetAll();
        Task<List<EvaluationPeriod>> GetActivePeriods();
        Task<List<EvaluationPeriod>> GetPeriodsByFilter(Expression<Func<EvaluationPeriod, bool>> filter);
    }
} 