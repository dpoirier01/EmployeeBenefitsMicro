using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeBenefits.Framework.Tasks
{
    public interface ITaskFactory
    {
        TTask Create<TTask, TContext>(TContext context);
    }
}
