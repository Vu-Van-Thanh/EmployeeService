
using EmployeeService.Core.Domain.Entities;
using EmployeeService.Core.RepositoryContracts;
using EmployeeService.Infrastructure.AppDbContext;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _dbcontext;

        public EmployeeRepository(ApplicationDbContext applicationDbContext)
        {
            _dbcontext = applicationDbContext;
        }

        public async Task<bool> AddEmployee(Employee employee)
        {
            await _dbcontext.Employees.AddAsync(employee);
            return await _dbcontext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteEmployee(Guid employeeId)
        {
            _dbcontext.Employees.Remove(new Employee { EmployeeID = employeeId });
            return await _dbcontext.SaveChangesAsync() > 0;
        }

        public async Task<List<Employee>> GetAll()
        {
            return await _dbcontext.Employees.ToListAsync();
        }

        public async Task<Employee?> GetEmployeeById(Guid Id)
        {
            Employee? employee =  await _dbcontext.Employees.FirstOrDefaultAsync(e => e.EmployeeID == Id);
            return employee;
        }

        public async  Task<Employee?> GetEmployeeIdByUserId(Guid Id)
        {
            return await _dbcontext.Employees.FirstOrDefaultAsync(e => e.AccountID == Id);
        }

        public async Task<Guid> UpdateEmployee(Employee employee)
        {
            Employee? existEmployee = await _dbcontext.Employees.FindAsync(employee.EmployeeID);
            if(existEmployee == null) return Guid.Empty;


            existEmployee.Address = employee.Address;

            existEmployee.IndentityCard = employee.IndentityCard ?? employee.IndentityCard;

            existEmployee.Ethnic = employee.Ethnic ?? employee.Ethnic;
           
            if (existEmployee.Country != employee.Country)
            {
                existEmployee.Country = employee.Country;
            }

            if (employee.DateOfBirth != null)
            {
                existEmployee.DateOfBirth = employee.DateOfBirth;
            }
            existEmployee.District = employee.District ?? employee.District;

            existEmployee.InsuranceNumber = employee.InsuranceNumber ?? employee.InsuranceNumber;

            existEmployee.FirstName = employee.FirstName ?? employee.FirstName;
            existEmployee.LastName = employee.LastName ?? employee.LastName;
            existEmployee.Gender = employee.Gender ?? employee.Gender;



            existEmployee.Nationality = employee.Nationality ?? employee.Nationality;

            existEmployee.PlaceIssued = employee.PlaceIssued ?? employee.PlaceIssued;

            existEmployee.PlaceOfBirth = employee.PlaceOfBirth ?? employee.PlaceOfBirth;

            existEmployee.Province = employee.Province ?? employee.Province;
            existEmployee.Religion = employee.Religion ?? employee.Religion;
            try
            {
                await _dbcontext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return Guid.Empty;
            }
            return existEmployee.EmployeeID;
            
        }
    }
}
