using MediatR;
using EmployeeBenefits.Commands.Models;
using AutoMapper;
using EmployeeBenefits.Data.Repositories;
using EmployeeBenefits.Data.Entities;

namespace EmployeeBenefits.Commands.Handlers
{
    public class AddNewEmployeeHandler : IRequestHandler<AddNewEmployee>
    {

        private readonly IEmployeeRepository _db;
        private readonly IMapper _mapper;

        public AddNewEmployeeHandler(IEmployeeRepository db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public void Handle(AddNewEmployee employee)
        {
            var addNewEmployee = _mapper.Map<AddNewEmployee, Employee>(employee);

            _db.Add(addNewEmployee);
        }
    }
}
