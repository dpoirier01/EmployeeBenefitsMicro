using EmployeeBenefits.Queries.Results;
using MediatR;
using System.Collections.Generic;

namespace EmployeeBenefits.Queries
{
    public class GetAllEmployeesMessage : IRequest<List<GetAllEmployeesResults>>
    {
    }
}
