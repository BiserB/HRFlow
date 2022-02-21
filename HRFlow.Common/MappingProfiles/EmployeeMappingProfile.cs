using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HRFlow.Common.ViewModels;
using HRFlow.Entities;

namespace HRFlow.Common.MappingProfiles
{
    public class EmployeeMappingProfile : Profile
    {
        public EmployeeMappingProfile()
        {
            this.CreateMap<Employee, EmployeeViewModel>();
        }
    }
}
