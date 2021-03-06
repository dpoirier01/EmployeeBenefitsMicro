﻿using EmployeeBenefits.Queries.Results;
using MediatR;

namespace EmployeeBenefits.Queries.Messages
{
    public class GetBenefitsDataMessage : IRequest<GetBenefitsDataResults>
    {
        public int EmployeeId { get; set; }
    }
}
