using EmployeeService.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.Domain.Entities
{
    public class EvaluationCriterion 
    {
        [Key]
        public Guid CriterionID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public double? Weight { get; set; }
        public string? Category { get; set; }

    }
}
