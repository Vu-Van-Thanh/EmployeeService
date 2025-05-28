using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EmployeeService.Core.Domain.Entities;

namespace EmployeeService.Core.RepositoryContracts
{
    public interface IRelativeRepository
    {
        Task<Relative> UpsertRelative(Relative relative);
        Task<List<Relative>> GetAllRelative();
        Task<List<Relative>?> GetRelativeById(Guid relativeId);
        Task<List<Relative>?> GetRelativeByFilter(Expression<Func<Relative,bool>> expression);
    }
}
