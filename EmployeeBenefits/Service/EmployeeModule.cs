using EmployeeBenefits.Commands.Models;
using EmployeeBenefits.Queries;
using EmployeeBenefits.Queries.Messages;
using EmployeeBenefits.Queries.Results;
using MediatR;
using Nancy;
using Nancy.ModelBinding;
using System.Collections.Generic;

namespace EmployeeBenefits.Service
{
    public class EmployeeModule : NancyModule
    {
        private readonly IMediator _mediator;

        public EmployeeModule(IMediator mediator)
        {
            _mediator = mediator;
            
            Post("/enroll", args =>
            {
                var employeeModel = this.Bind<AddNewEmployee>();
                SaveNewEmployee(employeeModel);
                return "Success";
            });

            Get("/getAllEmployees", results =>
            {
                var employeeList = this.GetAllEmployeesList();
                return employeeList;
            });

            Get("/benefitssummary/{employeeId}", _ =>
            {
                var employeeId = _.employeeId;
                return GetBenefitsSummary(employeeId);
            });
        }

        public void SaveNewEmployee(AddNewEmployee employee)
        {
            _mediator.Send(employee);
        }

        public List<GetAllEmployeesResults> GetAllEmployeesList()
        {
            var message = new GetAllEmployeesMessage();
            var results = _mediator.Send(message);

            return results.Result;
        }

        private Response GetBenefitsSummary(int employeeId)
        {
            if (employeeId < 1)
                return HttpStatusCode.BadRequest;

            var message = new GetBenefitsDataMessage { EmployeeId = employeeId };

            var data = _mediator.Send(message).Result;

            var returnValue = Response.AsJson(data);

            return returnValue;
        }
    }
}
