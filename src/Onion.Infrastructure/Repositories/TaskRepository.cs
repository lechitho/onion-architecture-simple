using Onion.Domain.Tasks.ValueObjects;
using Onion.Domain.Tasks;

namespace Onion.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ITaskFactory _taskFactory;

        public TaskRepository(ITaskFactory taskFactory)
        {
            _taskFactory = taskFactory;
        }

        public Task<Domain.Tasks.Task> Add(Domain.Tasks.Task taskEntity)
        {
            return System.Threading.Tasks.Task.FromResult(
                _taskFactory.CreateTaskInstance(new Summary("summary test"), new Description("description test")));
        }

        public Task<List<Domain.Tasks.Task>> FindAll()
        {
            var tasks = System.Threading.Tasks.Task.FromResult(new List<Domain.Tasks.Task> {
                _taskFactory.CreateTaskInstance(new Summary("summary test"), new Description("description test"))});

            return tasks;
        }

        public Task<Domain.Tasks.Task> FindById(Guid id)
        {
            return System.Threading.Tasks.Task.FromResult(
                _taskFactory.CreateTaskInstance(new Summary("summary test"), new Description("description test")));
        }

        public System.Threading.Tasks.Task Remove(Guid id)
        {
            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
