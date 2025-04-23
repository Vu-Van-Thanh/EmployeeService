using System.ComponentModel.DataAnnotations;
using EmployeeService.Core.Enums;

namespace EmployeeService.Core.Domain.Entities
{
    public class Employee
    {
        [Key]
        public Guid EmployeeID { get; set; }
        public string Position { get; set;}

        public Guid? ManagerID { get; set; }
        public Guid? AccountID { get; set; }
        public string? DepartmentID { get; set; }

        [StringLength(20)]
        public string? FirstName { get; set; }

        [StringLength(20)]
        public string? LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [StringLength(10)]
        public GenderOptions? Gender { get; set; }

        [StringLength(13)]
        public string Tax { get; set; }

        [StringLength(200)] //nvarchar(200)
        public string? Address { get; set; }


        public virtual ICollection<EmployeeMedia>? EmployeeMedia { get; set; }
        public virtual ICollection<EmployeeContract>? EmployeeContract { get; set; }

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
        public string? PlaceIssued { get; set; }

        public string? Country { get; set; }
        [StringLength(40)]
        public string? Province { get; set; }
        [StringLength(40)]
        public string? District { get; set; }
        [StringLength(40)]
        public string? Commune { get; set; }
        [StringLength(30)]
        public string? InsuranceNumber { get; set; }
        public override string ToString()
        {
            return $"Person ID: {EmployeeID}, Person Name: {FirstName + LastName}, Date of Birth: {DateOfBirth?.ToString("MM/dd/yyyy")}, Gender: {Gender}, Address: {Address}";
        }

    }
}
