using EmployeeService.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.DTO
{
    public class EvaluationCriterionDTO
    {
        public Guid CriterionID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public double? Weight { get; set; }
        public string? Category { get; set; }
    }

    public class EvaluationCriterionAddDTO
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public double? Weight { get; set; }
        public string? Category { get; set; }
    }

    public class EvaluationCriterionFilterDTO
    {
        public string? NameContains { get; set; }
        public string? Category { get; set; }

        public Expression<Func<EvaluationCriterion, bool>> ToExpression()
        {
            return e => 
                (string.IsNullOrEmpty(NameContains) || e.Name.Contains(NameContains)) &&
                (string.IsNullOrEmpty(Category) || e.Category == Category);
        }
    }
} 