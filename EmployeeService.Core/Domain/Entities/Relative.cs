using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EmployeeService.Core.Domain.Entities
{
    public class Relative
    {
        [Key]
        public Guid RelativeID { get; set; }

        public Guid? EmployeeID { get; set; }

        [StringLength(20)]
        public string? FirstName { get; set; }
        public string? RelativeType { get; set; }

        [StringLength(20)]
        public string? LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }


        [StringLength(200)] //nvarchar(200)
        public string? Address { get; set; }


        // Thông tin bổ sung
        [StringLength(100)]
        public string? Nationality { get; set; } = "Việt Nam";
        [StringLength(30)]
        public string? Ethnic { get; set; }

        [StringLength(50)]
        public string? Religion { get; set; } = "Không";

        public string? PlaceOfBirth { get; set; }

        // căn cước công dân
        public string? IndentityCard { get; set; }

        public string? Country { get; set; }
        [StringLength(40)]
        public string? Province { get; set; }
        [StringLength(40)]
        public string? District { get; set; }
        [StringLength(40)]
        public string? Commune { get; set; }

        public string? PhoneNumber { get; set; }
        public override string ToString()
        {
            return $"Person ID: {RelativeID}, Person Name: {FirstName + LastName}, Date of Birth: {DateOfBirth?.ToString("MM/dd/yyyy")}, Type : {RelativeType}, Address: {Address}";
        }
    }
}
