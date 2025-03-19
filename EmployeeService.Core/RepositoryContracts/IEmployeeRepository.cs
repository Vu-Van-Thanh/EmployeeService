using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeService.Core.Domain.Entities;

namespace EmployeeService.Core.RepositoryContracts
{
    public interface IEmployeeRepository
    {
        public Task<Employee> GetEmployeeById(Guid Id);
    }
}
