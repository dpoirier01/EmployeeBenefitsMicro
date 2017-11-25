using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace EmployeeBenefits.Commands.Models
{
    public class AddNewEmployee : IRequest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }
        public int NumberOfPayPeriods { get; set; }
    }
}
