using EmployeeService.Core.Domain.Entities;
using EmployeeService.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.Extension
{
    public static class EvaluationExtensions
    {
        public static EmployeeEvaluationDTO ToDTO(this EmployeeEvaluation evaluation)
        {
            return new EmployeeEvaluationDTO
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