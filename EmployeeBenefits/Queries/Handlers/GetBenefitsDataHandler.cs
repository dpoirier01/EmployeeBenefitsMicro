using EmployeeBenefits.Data;
using EmployeeBenefits.Data.Entities;
using EmployeeBenefits.Queries.Messages;
using EmployeeBenefits.Queries.Results;
using MediatR;
using System.Linq;

namespace EmployeeBenefits.Queries.Handlers
{
    public class GetBenefitsDataHandler : IRequestHandler<GetBenefitsDataMessage, GetBenefitsDataResults>
    {

        private readonly IBenefitsContext _db;

        public GetBenefitsDataHandler(IBenefitsContext db)
        {
            _db = db;
        }

        public GetBenefitsDataResults Handle(GetBenefitsDataMessage message)
        {
            var data = new GetBenefitsDataResults();
            data.Employee = GetEmployee(message.EmployeeId).FirstOrDefault();
            data.Dependent = GetDependent(message.EmployeeId).ToList();
            data.Benefit = GetBenefit().FirstOrDefault();
            data.Promotions = GetPromotions().ToList();

            return data;
        }

        private IQueryable<Employee> GetEmployee(int employeeId)
        {
            var employee = (from e in _db.Employee
                            where e.Id == employeeId
                            select new Employee()
                            {
                                Id = e.Id,
                                FirstName = e.FirstName,
                                LastName = e.LastName,
                                NumberOfPayPeriods = e.NumberOfPayPeriods,
                                Salary = e.Salary 
                            });
            return employee;
        }

        private IQueryable<Dependent> GetDependent(int employeeId)
        {
            var dependents = (from d in _db.Dependent
                             where d.EmployeeId == employeeId
                             select new Dependent
                             {
                                 Id = d.Id,
                                 FirstName = d.FirstName,
                                 LastName = d.LastName,
                                 EmployeeId = d.EmployeeId,
                                 Relationship = d.Relationship 
                             });
            return dependents;
        }

        private IQueryable<Benefit> GetBenefit()
        {
            var benefit = (from b in _db.Benefit
                           select new Benefit
                           {
                               EmployeeCost = b.EmployeeCost,
                               DependentCost = b.DependentCost 
                           });
            return benefit;
        }

        private IQueryable<Promotions> GetPromotions()
        {
            var promotions = (from p in _db.Promotions
                             select new Promotions
                             {
                                 Id = p.Id,
                                 PromotionName = p.PromotionName,
                                 PromotionTrigger = p.PromotionTrigger,
                                 DiscountAmount = p.DiscountAmount
                             });
            return promotions;
        }
    }
}
