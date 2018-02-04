namespace EmployeeBenefits.Framework.Tasks
{
    public interface ITaskFactory
    {
        TTask Create<TTask, TContext>(TContext context);
    }
}
