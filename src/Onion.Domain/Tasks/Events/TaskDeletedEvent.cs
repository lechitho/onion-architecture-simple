namespace Onion.Domain.Tasks.Events
{
    public class TaskDeletedEvent : TaskEvent
    {
        public TaskDeletedEvent(Guid id)
        {
            Id = id;
        }
    }
}
