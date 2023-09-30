using Onion.Domain.Tasks.ValueObjects;

namespace Onion.Domain.Tasks
{
    public interface ITaskFactory
    {
        Task CreateTaskInstance(Summary summary, Description description);
    }
}
