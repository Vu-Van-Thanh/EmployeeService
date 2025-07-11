﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.DTO
{
    public class EmployeeAddRequest
    {
        public string? FirstName { get; set; }

        public Guid? ManagerId { get; set; }
        public string? LastName { get; set; }
        public string? DepartmentId { get; set;}
        public Guid? AccountId { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? Gender { get; set; }

        public string Tax { get; set; }
        public string? Address { get; set; }

        public string? Nationality { get; set; } = "Việt Nam";

        public string? Ethnic { get; set; }

        public string? Religion { get; set; } = "Không";

        public string? PlaceOfBirth { get; set; }

        public string? IndentityCard { get; set; }
        public string? PlaceIssued { get; set; }

        public string? Country { get; set; }

        public string? Province { get; set; }

        public string? District { get; set; }

        public string? Commune { get; set; }
        public string? InsuranceNumber { get; set; }
    }
}
