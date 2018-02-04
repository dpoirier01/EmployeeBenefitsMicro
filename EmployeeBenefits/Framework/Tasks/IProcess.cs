namespace EmployeeBenefits.Framework.Tasks
{
    public interface IProcess<TContext> where TContext : class
    {
        IProcess<TContext> With(TContext context);
        IProcess<TContext> Do<TTask>() where TTask : ITask;
    }
}
