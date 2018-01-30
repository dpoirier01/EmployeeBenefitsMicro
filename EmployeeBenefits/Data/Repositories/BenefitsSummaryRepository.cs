using EmployeeBenefits.Queries.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeBenefits.Data.Repositories
{
    public class BenefitsSummaryRepository : GenericRepository<GetBenefitsSummaryResults>, IBenefitsSummaryRepository
    {
        public BenefitsSummaryRepository(BenefitsContext dbContext) : base(dbContext) { }
}
}
