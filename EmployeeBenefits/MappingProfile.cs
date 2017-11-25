using AutoMapper;
using EmployeeBenefits.Commands.Models;
using EmployeeBenefits.Data;

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
