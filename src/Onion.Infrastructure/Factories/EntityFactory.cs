using Onion.Domain.Tasks.ValueObjects;
using Onion.Domain.Tasks;

namespace Onion.Infrastructure.Factories
{
    public class EntityFactory : ITaskFactory
    {
        public Domain.Tasks.Task CreateTaskInstance(Summary summary, Description description)
        {
            return new TaskFactory(summary, description);
        }
    }
}
