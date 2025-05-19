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
    public class EmployeeEvaluation   
    {
        [Key]
        public Guid ID { get; set; }
        public Guid EmployeeID  { get; set; }
        public Guid EvaluatorId {get;set;}

        public Guid PeriodId {get;set;}
        public DateTime EvaluationDate {get;set;}
        public double TotalScore {get;set;}

        public string DetailJson {get;set;}
        [ForeignKey("EmployeeID")]
        public virtual Employee? Employee {get;set;}

        [ForeignKey("PeriodId")]
        public virtual EvaluationPeriod? Period {get;set;}

    }
}
