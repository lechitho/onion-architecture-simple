using Onion.Domain.Tasks.ValueObjects;

namespace Onion.Infrastructure.Factories
{
    public class TaskFactory : Domain.Tasks.Task
    {
        public TaskFactory()
        {

        }

        public TaskFactory(Summary summary, Description description)
        {
            TaskId = new TaskId(Guid.NewGuid());
            Summary = summary;
            Description = description;
        }
    }
}
