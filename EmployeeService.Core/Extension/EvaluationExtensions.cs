using EmployeeService.Core.Domain.Entities;
using EmployeeService.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmployeeService.Core.Extension
{
    public static class EvaluationExtensions
    {
        public static EmployeeEvaluationDTO ToDTO(this EmployeeEvaluation evaluation, IEnumerable<EvaluationCriterion>? criteria = null)
        {
            var dto = new EmployeeEvaluationDTO
            {
                ID = evaluation.ID,
                EmployeeID = evaluation.EmployeeID,
                EvaluatorId = evaluation.EvaluatorId,
                PeriodId = evaluation.PeriodId,
                EvaluationDate = evaluation.EvaluationDate,
                TotalScore = evaluation.TotalScore,
                DetailJson = evaluation.DetailJson,
                EmployeeName = evaluation.Employee != null ? $"{evaluation.Employee.FirstName} {evaluation.Employee.LastName}" : null,
                PeriodName = evaluation.Period?.Name
            };

            if (criteria != null && !string.IsNullOrEmpty(evaluation.DetailJson))
            {
                try
                {
                    var scoresDict = JsonSerializer.Deserialize<Dictionary<string, double>>(evaluation.DetailJson);
                    if (scoresDict != null)
                    {
                        var result = new Dictionary<string, double>();
                        foreach (var score in scoresDict)
                        {
                            // Convert both IDs to Guid for comparison
                            if (Guid.TryParse(score.Key, out Guid scoreId))
                            {
                                var criterion = criteria.FirstOrDefault(c => c.CriterionID == scoreId);
                                if (criterion != null)
                                {
                                    result[criterion.Name] = score.Value;
                                }
                            }
                        }
                        if (result.Any())
                        {
                            dto.DetailJson = JsonSerializer.Serialize(result);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error converting DetailJson: {ex.Message}");
                }
            }

            return dto;
        }

        public static EvaluationCriterionDTO ToDTO(this EvaluationCriterion criterion)
        {
            return new EvaluationCriterionDTO
            {
                CriterionID = criterion.CriterionID,
                Name = criterion.Name,
                Description = criterion.Description,
                Weight = criterion.Weight,
                Category = criterion.Category
            };
        }

        public static EvaluationPeriodDTO ToDTO(this EvaluationPeriod period)
        {
            return new EvaluationPeriodDTO
            {
                PeriodID = period.PeriodID,
                Name = period.Name,
                StartDate = period.StartDate,
                EndDate = period.EndDate
            };
        }
    }
} 