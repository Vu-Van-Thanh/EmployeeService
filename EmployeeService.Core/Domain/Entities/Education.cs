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
    public class Education
    {
        [Key]
        public Guid EducationID { get; set; }
        public Guid EmployeeID { get; set; }
        public DegreeLevels? Degree { get; set; }
        public string? Major { get; set; }
        public string? School { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Description { get; set; }
        [ForeignKey("EmployeeID")]
        public virtual Employee? Employee { get; set; }
    }
}
