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

        public async Task<List<Education>?> GetEducationByEmployeeId(Guid Id)
        {
            return await _context.Educations
                .Where(e => e.EmployeeID == Id)
                .ToListAsync(); 
        }

        public async Task<Education?> GetEducationById(Guid Id)
        {
            return await _context.Educations
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(e => e.EducationID == Id);
        }

        public async Task<List<Education>> GetEducationsByFilter(Expression<Func<Education, bool>> filter)
        {
            return  await _context.Educations
                .Where(filter)
                .ToListAsync();
        }

        public async Task<Guid> UpdateEducation(Education education)
        {
            var existing = await _context.Educations.FindAsync(education.EducationID);
            if (existing == null)
            {
                throw new Exception("Education not found");
            }

            // Cập nhật các trường cần thiết
            existing.School = education.School;
            existing.Degree = education.Degree;
            existing.Major = education.Major;
            existing.StartDate = education.StartDate;
            existing.EndDate = education.EndDate;
            existing.Description = education.Description;


            await _context.SaveChangesAsync();

            return existing.EducationID;
        }

    }
}
