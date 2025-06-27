using EmployeeService.Core.Domain.Entities;
using EmployeeService.Core.DTO;
using EmployeeService.Core.Extension;
using EmployeeService.Core.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmployeeService.Core.Services
{
    public interface IEmployeeEvaluationService
    {
        Task<List<EmployeeEvaluationDTO>> GetAllEvaluations();
        Task<EmployeeEvaluationDTO?> GetEvaluationById(Guid id);
        Task<List<EmployeeEvaluationDTO>> GetEvaluationsByEmployeeId(Guid employeeId);
        Task<List<EncryptEvaluationDTO>> GetEncryptEvaluationsByEmployee(Guid employeeId);
        Task<List<EncryptEvaluationDTO>> GetEncryptEvaluationsByCriterior(Guid employeeId);
        Task<List<EmployeeEvaluationDTO>> GetEvaluationsByPeriodId(Guid periodId);
        Task<List<EmployeeEvaluationDTO>> GetEvaluationsByFilter(EmployeeEvaluationFilterDTO filter);
        Task<Guid> AddEvaluation(EmployeeEvaluationAddDTO evaluation);
        Task<Guid> UpdateEvaluation(EmployeeEvaluationDTO evaluation);
        Task<bool> DeleteEvaluation(Guid id);
        Task<string> GetDetailedScoresWithNames(Guid evaluationId);
        Task<List<CriterionStatisticDTO>> GetCriterionStatistics();
    }

    public class EmployeeEvaluationService : IEmployeeEvaluationService
    {
        private readonly IEmployeeEvaluationRepository _evaluationRepository;
        private readonly IEvaluationCriterionRepository _criterionRepository;
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeEvaluationService(
            IEmployeeEvaluationRepository evaluationRepository,
            IEvaluationCriterionRepository criterionRepository,
            IEmployeeRepository employeeRepository)
        {
            _evaluationRepository = evaluationRepository;
            _criterionRepository = criterionRepository;
            _employeeRepository = employeeRepository;
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
                DetailJson = evaluation.DetailJson,
                DetailJsonManager = evaluation.DetailJson
            };

            return await _evaluationRepository.AddEvaluation(result);
        }

        public async Task<bool> DeleteEvaluation(Guid id)
        {
            return await _evaluationRepository.DeleteEvaluation(id);
        }

        public async Task<List<EmployeeEvaluationDTO>> GetAllEvaluations()
        {
            var evaluations = await _evaluationRepository.GetAll();
            var criteria = await _criterionRepository.GetAll();
            return evaluations.Select(e => e.ToDTO(criteria)).ToList();
        }

        public async Task<EmployeeEvaluationDTO?> GetEvaluationById(Guid id)
        {
            var evaluation = await _evaluationRepository.GetEvaluationById(id);
            if (evaluation == null)
                return null;
            
            var criteria = await _criterionRepository.GetAll();
            return evaluation.ToDTO(criteria);
        }

        public async Task<List<EmployeeEvaluationDTO>> GetEvaluationsByEmployeeId(Guid employeeId)
        {
            var evaluations = await _evaluationRepository.GetEvaluationsByEmployeeId(employeeId);
            var criteria = await _criterionRepository.GetAll();
            return evaluations.Select(e => e.ToDTO(criteria)).ToList();
        }

        public async Task<List<EmployeeEvaluationDTO>> GetEvaluationsByFilter(EmployeeEvaluationFilterDTO filter)
        {
            var evaluations = await _evaluationRepository.GetEvaluationsByFilter(filter.ToExpression());
            var criteria = await _criterionRepository.GetAll();
            return evaluations.Select(e => e.ToDTO(criteria)).ToList();
        }

        public async Task<List<EmployeeEvaluationDTO>> GetEvaluationsByPeriodId(Guid periodId)
        {
            var evaluations = await _evaluationRepository.GetEvaluationsByPeriodId(periodId);
            var criteria = await _criterionRepository.GetAll();
            return evaluations.Select(e => e.ToDTO(criteria)).ToList();
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
                    DetailJson = evaluation.DetailJson,
                    DetailJsonManager = evaluation.DetailJsonManager,
                    Status = evaluation.Status
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

        public async Task<string> GetDetailedScoresWithNames(Guid evaluationId)
        {
            var evaluation = await _evaluationRepository.GetEvaluationById(evaluationId);
            if (evaluation == null || string.IsNullOrEmpty(evaluation.DetailJson))
                return "{}";

            var criteria = await _criterionRepository.GetAll();
            var scoresDict = JsonSerializer.Deserialize<Dictionary<string, double>>(evaluation.DetailJson);
            if (scoresDict == null)
                return "{}";

            var result = new Dictionary<string, double>();
            foreach (var score in scoresDict)
            {
                var criterion = criteria.FirstOrDefault(c => c.CriterionID.ToString() == score.Key);
                if (criterion != null)
                {
                    result[criterion.Name] = score.Value;
                }
            }

            return JsonSerializer.Serialize(result);
        }

        public async Task<List<CriterionStatisticDTO>> GetCriterionStatistics()
        {
            // Lấy tất cả tiêu chí trước
            var criteria = await _criterionRepository.GetAll();
            var evaluations = await _evaluationRepository.GetAll();
            var result = new List<CriterionStatisticDTO>();

            // Với mỗi tiêu chí
            foreach (var criterion in criteria)
            {
                var scores = new List<double>();
                
                // Tìm trong tất cả đánh giá
                foreach (var evaluation in evaluations)
                {
                    if (!string.IsNullOrEmpty(evaluation.DetailJson))
                    {
                        try
                        {
                            var scoresDict = JsonSerializer.Deserialize<Dictionary<string, double>>(evaluation.DetailJson);
                            if (scoresDict != null && scoresDict.TryGetValue(criterion.CriterionID.ToString().ToUpper(), out double score))
                            {
                                scores.Add(score);
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }

                // Tính trung bình và thêm vào kết quả
                result.Add(new CriterionStatisticDTO
                {
                    CriterionID = criterion.CriterionID,
                    Name = criterion.Name,
                    Category = criterion.Category,
                    Weight = criterion.Weight,
                    AverageScore = scores.Any() ? scores.Average() : 0,
                    TotalEvaluations = scores.Count
                });
            }

            return result;
        }

        public async Task<List<EncryptEvaluationDTO>> GetEncryptEvaluationsByEmployee(Guid employeeId)
        {
            var evaluations = await _evaluationRepository.GetEvaluationsByEmployeeId(employeeId);
            var ToDTO  = evaluations.Select(e => e.ToDTOEncrypt()).ToList();
            foreach(var evaluation in ToDTO)
            {
                var employee = await _employeeRepository.GetEmployeeById(evaluation.EmployeeID);
                if (employee != null)
                {
                    evaluation.EmployeeName = $"{employee.FirstName} {employee.LastName}";
                }

                var evaluator = await _employeeRepository.GetEmployeeById(evaluation.EvaluatorId);
                if (evaluator != null)
                {
                    evaluation.EvaluatorName = $"{evaluator.FirstName} {evaluator.LastName}";
                }
            }
            return ToDTO;
        }

        public async  Task<List<EncryptEvaluationDTO>> GetEncryptEvaluationsByCriterior(Guid employeeId)
        {
            var evaluations = await _evaluationRepository.GetEvaluationsByCriterior(employeeId);
            var ToDTO =  evaluations.Select(e => e.ToDTOEncrypt()).ToList();
            foreach (var evaluation in ToDTO)
            {
                var employee = await _employeeRepository.GetEmployeeById(evaluation.EmployeeID);
                if (employee != null)
                {
                    evaluation.EmployeeName = $"{employee.FirstName} {employee.LastName}";
                }

                var evaluator = await _employeeRepository.GetEmployeeById(evaluation.EvaluatorId);
                if (evaluator != null)
                {
                    evaluation.EvaluatorName = $"{evaluator.FirstName} {evaluator.LastName}";
                }
            }
            return ToDTO;
        }
    }
} 