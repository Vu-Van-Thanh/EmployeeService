using System.Linq.Expressions;
using EmployeeService.Core.Domain.Entities;

namespace EmployeeService.Core.RepositoryContracts
{
    public interface IEducationRepository
    {
        Task<Guid> AddEducation(Education education);
        Task<Guid> UpdateEducation(Education education);
        Task<bool> DeleteEducation(Guid employeeId);
        Task<Education?> GetEducationById(Guid Id);
        Task<List<Education>?> GetEducationByEmployeeId(Guid Id);
        Task<List<Education>> GetAll();
        Task<List<Education>> GetEducationsByFilter(Expression<Func<Education, bool>> filter);
    }
}
