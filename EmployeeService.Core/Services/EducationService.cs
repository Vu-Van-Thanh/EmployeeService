using EmployeeService.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.Services
{
    public interface IEducationService
    {
        Task<List<EducationDTO>> GetAllEducations();
        Task<EducationDTO?> GetEducationById(Guid Id);
        Task<List<EducationDTO>?> GetEducationByEmployeeId(Guid Id);
        Task<List<EducationDTO>> GetEducationsByFilter(Func<EducationFilterDTO, bool> filter);
        Task<Guid> AddEducation(EducationDTO education);
        Task<bool> UpdateEducation(EducationDTO education);
        Task<bool> DeleteEducation(Guid employeeId);
    }

    public class EducationService : IEducationService
    {
        public Task<Guid> AddEducation(EducationDTO education)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteEducation(Guid employeeId)
        {
            throw new NotImplementedException();
        }

        public Task<List<EducationDTO>> GetAllEducations()
        {
            throw new NotImplementedException();
        }

        public Task<List<EducationDTO>?> GetEducationByEmployeeId(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<EducationDTO?> GetEducationById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<EducationDTO>> GetEducationsByFilter(Func<EducationFilterDTO, bool> filter)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateEducation(EducationDTO education)
        {
            throw new NotImplementedException();
        }
    }
}
