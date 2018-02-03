using EmployeeBenefits.Queries.Results;

namespace EmployeeBenefits.Business
{
    public interface ISummarizeBenefits
    {
        BenefitsSummary Run(GetBenefitsDataResults data);
    }
}
