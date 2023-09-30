namespace Onion.Domain.Tasks.Commands
{
    public class DeleteTaskCommand : TaskCommand
    {
        public DeleteTaskCommand(Guid id)
        {
            Id = id;
        }
    }
}
