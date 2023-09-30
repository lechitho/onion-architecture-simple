using Onion.Domain.Tasks.ValueObjects;

namespace Onion.Domain.Tasks
{
    public class Task : IAggregateRoot
    {
        public TaskId TaskId { get; set; }

        public Summary Summary { get; set; }

        public Description Description { get; set; }
    }
}
