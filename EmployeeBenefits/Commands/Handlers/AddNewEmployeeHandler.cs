using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using EmployeeBenefits.Commands.Models;
using AutoMapper;
using EmployeeBenefits.Data;

namespace EmployeeBenefits.Commands.Handlers
{
    public class AddNewEmployeeHandler : IRequestHandler<AddNewEmployee>
    {

        private readonly BenefitsContext _db;
        private readonly IMapper _mapper;

        public AddNewEmployeeHandler(BenefitsContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public void Handle(AddNewEmployee employee)
        {
            var addNewEmployee = _mapper.Map<AddNewEmployee, Employee>(employee);

            _db.Add(addNewEmployee);
            _db.SaveChanges();
        }
    }
}
