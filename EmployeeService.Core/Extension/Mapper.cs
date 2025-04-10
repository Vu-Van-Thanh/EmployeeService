using AutoMapper;
using EmployeeService.Core.Domain.Entities;
using EmployeeService.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.Extension
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeImportDTO, Employee>();
            CreateMap<Employee, EmployeeImportDTO>();

        }
    }
}
