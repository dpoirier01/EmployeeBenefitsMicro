using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeBenefits.Framework.Tasks
{
    public interface IProcess<TContext> where TContext : class
    {
        IProcess<TContext> With(TContext context);
        IProcess<TContext> Do<TTask>() where TTask : ITask;
    }
}
