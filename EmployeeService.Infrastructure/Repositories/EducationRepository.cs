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
    public class EducationRepository : IEducationRepository
    {
        private readonly ApplicationDbContext _context;
        public EducationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> AddEducation(Education education)
        {
            try
            {
                _context.Educations.Add(education);
                await _context.SaveChangesAsync();
                return education.EducationID;
            }
            catch (Exception ex)
            {
                // Handle exception
                return Guid.Empty;
            }
            
        }

        public async Task<bool> DeleteEducation(Guid EducationId)
        {
            var education = await _context.Educations.FindAsync(EducationId);
            if (education == null)
                return false;

            _context.Educations.Remove(education);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Education>> GetAll()
        {
            return await _context.Educations.ToListAsync();
        }

        public Task<List<Education>?> GetEducationByEmployeeId(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<Education?> GetEducationById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Education>> GetEducationsByFilter(Expression<Func<Education, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> UpdateEducation(Education education)
        {
            throw new NotImplementedException();
        }
    }
}
