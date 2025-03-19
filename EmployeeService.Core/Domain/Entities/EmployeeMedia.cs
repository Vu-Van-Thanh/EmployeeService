using System.ComponentModel.DataAnnotations.Schema;


namespace EmployeeService.Core.Domain.Entities
{
    public class EmployeeMedia
    {
        public Guid EmployeeMediaID { get; set; }

        public string MediaType { get; set; }
        public Guid EmployeeID { get; set; }
        public string MediaUrl { get; set; }

        [ForeignKey("EmployeeID")]
        public virtual Employee employee { get; set; }
    }
}
