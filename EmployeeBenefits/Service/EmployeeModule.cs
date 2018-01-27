using EmployeeBenefits.Commands.Models;
using EmployeeBenefits.Queries;
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
            var data = new List<BenefitsSummary>
            {
                new BenefitsSummary
                {
                    EmployeeName = "sam",
                    EmployeeSalary = 1000,
                    EmployeeCost = 1000,
                    AnnualTotal= 1000,
                    BiWeeklyTotal = 1000,
                    Savings = 1,
                    DiscountAmount = 1,
                    DependentSummaryList = new List<DependentSummary>
                    {
                        new DependentSummary
                        {
                            DependentName = "larry",
                            DependentCost = 500,
                            DiscountFlag = true,
                            Relationship = "Son"
                        }
                    }
                }
            };

            var returnValue = Response.AsJson(data);

            return returnValue;
        }
        public class BenefitsSummary
        {
            public string EmployeeName { get; set; }
            public decimal EmployeeSalary { get; set; }
            public decimal EmployeeCost { get; set; }
            public decimal AnnualTotal { get; set; }
            public decimal BiWeeklyTotal { get; set; }
            public decimal Savings { get; set; }
            public decimal DiscountAmount { get; set; }
            public List<DependentSummary> DependentSummaryList { get; set; }
        }

        public class DependentSummary
        {
            public string DependentName { get; set; }
            public string Relationship { get; set; }
            public decimal DependentCost { get; set; }
            public bool DiscountFlag { get; set; }
        }
    }
}
