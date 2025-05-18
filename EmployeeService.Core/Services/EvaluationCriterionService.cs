using EmployeeService.Core.Domain.Entities;
using EmployeeService.Core.DTO;
using EmployeeService.Core.Extension;
using EmployeeService.Core.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.Services
{
    public interface IEvaluationCriterionService
    {
        Task<List<EvaluationCriterionDTO>> GetAllCriteria();
        Task<EvaluationCriterionDTO?> GetCriterionById(Guid id);
        Task<List<EvaluationCriterionDTO>> GetCriterionsByCategory(string category);
        Task<List<EvaluationCriterionDTO>> GetCriterionsByFilter(EvaluationCriterionFilterDTO filter);
        Task<Guid> AddCriterion(EvaluationCriterionAddDTO criterion);
        Task<Guid> UpdateCriterion(EvaluationCriterionDTO criterion);
        Task<bool> DeleteCriterion(Guid id);
    }

    public class EvaluationCriterionService : IEvaluationCriterionService
    {
        private readonly IEvaluationCriterionRepository _criterionRepository;

        public EvaluationCriterionService(IEvaluationCriterionRepository criterionRepository)
        {
            _criterionRepository = criterionRepository;
        }

        public async Task<Guid> AddCriterion(EvaluationCriterionAddDTO criterion)
        {
            EvaluationCriterion result = new EvaluationCriterion
            {
                Name = criterion.Name,
                Description = criterion.Description,
                Weight = criterion.Weight,
                Category = criterion.Category
            };

            return await _criterionRepository.AddCriterion(result);
        }

        public async Task<bool> DeleteCriterion(Guid id)
        {
            return await _criterionRepository.DeleteCriterion(id);
        }

        public async Task<List<EvaluationCriterionDTO>> GetAllCriteria()
        {
            List<EvaluationCriterion> criteria = await _criterionRepository.GetAll();
            return criteria.Select(c => c.ToDTO()).ToList();
        }

        public async Task<EvaluationCriterionDTO?> GetCriterionById(Guid id)
        {
            EvaluationCriterion? criterion = await _criterionRepository.GetCriterionById(id);
            if (criterion == null)
                return null;
            
            return criterion.ToDTO();
        }

        public async Task<List<EvaluationCriterionDTO>> GetCriterionsByCategory(string category)
        {
            List<EvaluationCriterion> criteria = await _criterionRepository.GetCriterionsByCategory(category);
            return criteria.Select(c => c.ToDTO()).ToList();
        }

        public async Task<List<EvaluationCriterionDTO>> GetCriterionsByFilter(EvaluationCriterionFilterDTO filter)
        {
            List<EvaluationCriterion> criteria = await _criterionRepository.GetCriterionsByFilter(filter.ToExpression());
            return criteria.Select(c => c.ToDTO()).ToList();
        }

        public async Task<Guid> UpdateCriterion(EvaluationCriterionDTO criterion)
        {
            try
            {
                await _criterionRepository.UpdateCriterion(new EvaluationCriterion
                {
                    CriterionID = criterion.CriterionID,
                    Name = criterion.Name,
                    Description = criterion.Description,
                    Weight = criterion.Weight,
                    Category = criterion.Category
                });
                
                return criterion.CriterionID;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error updating criterion: {ex.Message}");
                return Guid.Empty;
            }
        }
    }
} 