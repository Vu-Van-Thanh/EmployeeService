using EmployeeService.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.DTO
{
    public class EmployeeEvaluationDTO
    {
        public Guid ID { get; set; }
        public Guid EmployeeID { get; set; }
        public Guid EvaluatorId { get; set; }
        public Guid PeriodId { get; set; }
        public DateTime EvaluationDate { get; set; }
        public double TotalScore { get; set; }
        public string DetailJson { get; set; }
        public string? EmployeeName { get; set; }
        public string? PeriodName { get; set; }
    }

    public class EncryptEvaluationDTO
    {
        public Guid ID { get; set; }
        public Guid EmployeeID { get; set; }
        public Guid EvaluatorId { get; set; }
        public Guid PeriodId { get; set; }
        public DateTime EvaluationDate { get; set; }
        public double TotalScore { get; set; }
        public string DetailJson { get; set; }
        public string DetailJsonManager { get; set; }
        public string? PeriodName { get; set; }
    }

    public static class EmployeeEvaluationDTOExtensions
    {
        public static EncryptEvaluationDTO ToDTOEncrypt(this EmployeeEvaluation evaluation)
        {
            return new EncryptEvaluationDTO
            {
                ID = evaluation.ID,
                EmployeeID = evaluation.EmployeeID,
                EvaluatorId = evaluation.EvaluatorId,
                PeriodId = evaluation.PeriodId,
                EvaluationDate = evaluation.EvaluationDate,
                TotalScore = evaluation.TotalScore,
                DetailJson = evaluation.DetailJson,
                DetailJsonManager = evaluation.DetailJsonManager, 
                PeriodName = evaluation.Period?.Name 
            };
        }
    }

    public class EmployeeEvaluationAddDTO
    {
        public Guid EmployeeID { get; set; }
        public Guid EvaluatorId { get; set; }
        public Guid PeriodId { get; set; }
        public DateTime EvaluationDate { get; set; }
        public double TotalScore { get; set; }
        public string DetailJson { get; set; }
    }

    public class EmployeeEvaluationFilterDTO
    {
        public Guid? EmployeeID { get; set; }
        public Guid? PeriodId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public Expression<Func<EmployeeEvaluation, bool>> ToExpression()
        {
            return e => 
                (EmployeeID == null || e.EmployeeID == EmployeeID) &&
                (PeriodId == null || e.PeriodId == PeriodId) &&
                (StartDate == null || e.EvaluationDate >= StartDate) &&
                (EndDate == null || e.EvaluationDate <= EndDate);
        }
    }
} 