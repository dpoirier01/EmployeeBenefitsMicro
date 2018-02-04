using EmployeeBenefits.Data;
using EmployeeBenefits.Queries.Results;
using MediatR;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeBenefits.Queries.Handlers
{
    public class GetAllEmployeesHandler : IRequestHandler<GetAllEmployeesMessage, List<GetAllEmployeesResults>>
    {
        private readonly BenefitsContext _db;

        public GetAllEmployeesHandler(BenefitsContext db)
        {
            _db = db;
        }

        public List<GetAllEmployeesResults> Handle(GetAllEmployeesMessage message)
        {
            var emps = (from e in _db.Employee
                        orderby e.LastName
                        select new GetAllEmployeesResults()
                        {
                            Id = e.Id,
                            FirstName = e.FirstName,
                            LastName = e.LastName
                        }).ToList();
            return emps;
        }
    }
}
