namespace EmployeeBenefits.Framework.Tasks
{
    public class Process<TContext> : IProcess<TContext> where TContext : class
    {
        private readonly ITaskFactory taskFactory;
        private TContext context;

        public Process(ITaskFactory taskFactory)
        {
            this.taskFactory = taskFactory;
        }

        public IProcess<TContext> With(TContext context)
        {
            this.context = context;
            return this;
        }

        public IProcess<TContext> Do<TTask>() where TTask : ITask
        {
            var task = taskFactory.Create<TTask, TContext>(context);
            task.Run();
            return this;
        }
    }
}
