using EmployeeService.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.DTO
{
    public class EvaluationPeriodDTO
    {
        public Guid PeriodID { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class EvaluationPeriodAddDTO
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class EvaluationPeriodFilterDTO
    {
        public string? NameContains { get; set; }
        public DateTime? StartDateFrom { get; set; }
        public DateTime? StartDateTo { get; set; }
        public DateTime? EndDateFrom { get; set; }
        public DateTime? EndDateTo { get; set; }
        public bool? IsActive { get; set; }

        public Expression<Func<EvaluationPeriod, bool>> ToExpression()
        {
            return e => 
                (string.IsNullOrEmpty(NameContains) || e.Name.Contains(NameContains)) &&
                (StartDateFrom == null || e.StartDate >= StartDateFrom) &&
                (StartDateTo == null || e.StartDate <= StartDateTo) &&
                (EndDateFrom == null || e.EndDate >= EndDateFrom) &&
                (EndDateTo == null || e.EndDate <= EndDateTo) &&
                (IsActive == null || (IsActive.Value && DateTime.Now >= e.StartDate && DateTime.Now <= e.EndDate));
        }
    }
} 