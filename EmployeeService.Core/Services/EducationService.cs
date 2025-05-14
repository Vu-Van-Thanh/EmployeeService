using EmployeeService.Core.Domain.Entities;
using EmployeeService.Core.DTO;
using EmployeeService.Core.Enums;
using EmployeeService.Core.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.Services
{
    public interface IEducationService
    {
        Task<List<EducationDTO>> GetAllEducations();
        Task<EducationDTO?> GetEducationById(Guid Id);
        Task<List<EducationDTO>?> GetEducationByEmployeeId(Guid Id);
        Task<List<EducationDTO>> GetEducationsByFilter(EducationFilterDTO filter);
        Task<Guid> AddEducation(EducationAddDTO education);
        Task<bool> UpdateEducation(EducationDTO education);
        Task<bool> DeleteEducation(Guid employeeId);
    }

    public class EducationService : IEducationService
    {
        private readonly IEducationRepository _educationRepository;

        public EducationService(IEducationRepository educationRepository)
        {
            _educationRepository = educationRepository;
        }

        public async Task<Guid> AddEducation(EducationAddDTO education)
        {
            Education result = new Education
            {
                EmployeeID = education.EmployeeID,
                Degree = education.Degree != null ? Enum.Parse<DegreeLevels>(education.Degree) : DegreeLevels.Other,
                Major = education.Major,
                School = education.School,
                StartDate = education.StartDate,
                EndDate = education.EndDate,
                Description = education.Description
            };
            await _educationRepository.AddEducation(result);
            return result.EducationID;

        }

        public Task<bool> DeleteEducation(Guid EducatonId)
        {
            return _educationRepository.DeleteEducation(EducatonId);
        }

        public async  Task<List<EducationDTO>> GetAllEducations()
        {
            List<Education> edulist= await _educationRepository.GetAll();
            return edulist.Select( e => e.ToDTO()).ToList();

        }

        public async Task<List<EducationDTO>?> GetEducationByEmployeeId(Guid Id)
        {
            Expression<Func<Education, bool>> filter = e => e.EmployeeID == Id;
            List<Education> result = await _educationRepository.GetEducationsByFilter(filter);
            return result.Select(e => e.ToDTO()).ToList();
        }

        public async Task<EducationDTO?> GetEducationById(Guid Id)
        {
            Education education = await _educationRepository.GetEducationById(Id);
            if (education == null)
                return null;
            return education.ToDTO();
        }

        public async Task<List<EducationDTO>> GetEducationsByFilter(EducationFilterDTO filter)
        {
            List<Education> education = await _educationRepository.GetEducationsByFilter(filter.ToExpression());
            return education.Select(e => e.ToDTO()).ToList();
        }

        public async Task<bool> UpdateEducation(EducationDTO education)
        {
            try
            {
                await _educationRepository.UpdateEducation(new Education
                {
                    EducationID = education.EducationID,
                    EmployeeID = education.EmployeeID,
                    Degree = education.Degree != null ? Enum.Parse<DegreeLevels>(education.Degree) : DegreeLevels.Other,
                    Major = education.Major,
                    School = education.School,
                    StartDate = education.StartDate,
                    EndDate = education.EndDate,
                    Description = education.Description
                });
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error updating education: {ex.Message}");
                return false;
            }

        }
    }
}
