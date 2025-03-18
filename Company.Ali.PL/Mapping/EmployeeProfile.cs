using AutoMapper;
using Company.Ali.DAL.Models;
using Company.Ali.PL.Dtos;

namespace Company.Ali.PL.Mapping
{

    // CLR
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile() 
        {
            CreateMap<CreateEmployeeDto, Employee>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.EmpName));
            CreateMap<Employee, CreateEmployeeDto>()
                  .ForMember(d => d.EmpName, o => o.MapFrom(s => s.Name)); ;
        }

    }
}
