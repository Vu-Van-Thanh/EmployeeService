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