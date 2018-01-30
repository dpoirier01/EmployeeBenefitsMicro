using EmployeeBenefits.Data;
using EmployeeBenefits.Data.Repositories;
using EmployeeBenefits.Queries.Messages;
using EmployeeBenefits.Queries.Results;
using MediatR;
using System.Collections.Generic;

namespace EmployeeBenefits.Queries.Handlers
{
    public class GetBenefitsSummaryHandler : IRequestHandler<GetBenefitsSummaryMessage, List<GetBenefitsSummaryResults>>
    {

        private readonly IBenefitsSummaryRepository _db;

        public GetBenefitsSummaryHandler(IBenefitsSummaryRepository db)
        {
            _db = db;
        }

        public List<GetBenefitsSummaryResults> Handle(GetBenefitsSummaryMessage message)
        {
            var data = new List<GetBenefitsSummaryResults>
            {
                new GetBenefitsSummaryResults
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

            return data;
        }
    }
}
