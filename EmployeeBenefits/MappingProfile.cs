using AutoMapper;
using EmployeeBenefits.Commands.Models;
using EmployeeBenefits.Data.Entities;

namespace EmployeeBenefits
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddNewEmployee, Employee>();
        }
    }
}
