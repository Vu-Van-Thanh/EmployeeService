using EmployeeService.Core.Domain.Entities;
using EmployeeService.Core.DTO;
using EmployeeService.Core.Extension;
using EmployeeService.Core.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.Services
{
    public interface IEmployeeEvaluationService
    {
        Task<List<EmployeeEvaluationDTO>> GetAllEvaluations();
        Task<EmployeeEvaluationDTO?> GetEvaluationById(Guid id);
        Task<List<EmployeeEvaluationDTO>> GetEvaluationsByEmployeeId(Guid employeeId);
        Task<List<EmployeeEvaluationDTO>> GetEvaluationsByPeriodId(Guid periodId);
        Task<List<EmployeeEvaluationDTO>> GetEvaluationsByFilter(EmployeeEvaluationFilterDTO filter);
        Task<Guid> AddEvaluation(EmployeeEvaluationAddDTO evaluation);
        Task<Guid> UpdateEvaluation(EmployeeEvaluationDTO evaluation);
        Task<bool> DeleteEvaluation(Guid id);
    }

    public class EmployeeEvaluationService : IEmployeeEvaluationService
    {
        private readonly IEmployeeEvaluationRepository _evaluationRepository;

        public EmployeeEvaluationService(IEmployeeEvaluationRepository evaluationRepository)
        {
            _evaluationRepository = evaluationRepository;
        }

        public async Task<Guid> AddEvaluation(EmployeeEvaluationAddDTO evaluation)
        {
            EmployeeEvaluation result = new EmployeeEvaluation
            {
                EmployeeID = evaluation.EmployeeID,
                EvaluatorId = evaluation.EvaluatorId,
                PeriodId = evaluation.PeriodId,
                EvaluationDate = evaluation.EvaluationDate,
                TotalScore = evaluation.TotalScore,
                DetailJson = evaluation.DetailJson
            };

            return await _evaluationRepository.AddEvaluation(result);
        }

        public async Task<bool> DeleteEvaluation(Guid id)
        {
            return await _evaluationRepository.DeleteEvaluation(id);
        }

        public async Task<List<EmployeeEvaluationDTO>> GetAllEvaluations()
        {
            List<EmployeeEvaluation> evaluations = await _evaluationRepository.GetAll();
            return evaluations.Select(e => e.ToDTO()).ToList();
        }

        public async Task<EmployeeEvaluationDTO?> GetEvaluationById(Guid id)
        {
            EmployeeEvaluation? evaluation = await _evaluationRepository.GetEvaluationById(id);
            if (evaluation == null)
                return null;
            
            return evaluation.ToDTO();
        }

        public async Task<List<EmployeeEvaluationDTO>> GetEvaluationsByEmployeeId(Guid employeeId)
        {
            List<EmployeeEvaluation> evaluations = await _evaluationRepository.GetEvaluationsByEmployeeId(employeeId);
            return evaluations.Select(e => e.ToDTO()).ToList();
        }

        public async Task<List<EmployeeEvaluationDTO>> GetEvaluationsByFilter(EmployeeEvaluationFilterDTO filter)
        {
            List<EmployeeEvaluation> evaluations = await _evaluationRepository.GetEvaluationsByFilter(filter.ToExpression());
            return evaluations.Select(e => e.ToDTO()).ToList();
        }

        public async Task<List<EmployeeEvaluationDTO>> GetEvaluationsByPeriodId(Guid periodId)
        {
            List<EmployeeEvaluation> evaluations = await _evaluationRepository.GetEvaluationsByPeriodId(periodId);
            return evaluations.Select(e => e.ToDTO()).ToList();
        }

        public async Task<Guid> UpdateEvaluation(EmployeeEvaluationDTO evaluation)
        {
            try
            {
                await _evaluationRepository.UpdateEvaluation(new EmployeeEvaluation
                {
                    ID = evaluation.ID,
                    EmployeeID = evaluation.EmployeeID,
                    EvaluatorId = evaluation.EvaluatorId,
                    PeriodId = evaluation.PeriodId,
                    EvaluationDate = evaluation.EvaluationDate,
                    TotalScore = evaluation.TotalScore,
                    DetailJson = evaluation.DetailJson
                });
                
                return evaluation.ID;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error updating evaluation: {ex.Message}");
                return Guid.Empty;
            }
        }
    }
} 