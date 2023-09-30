using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Onion.Application.ViewModels;
using Onion.Domain.Tasks;
using Onion.Domain.Tasks.Commands;
using Task = Onion.Domain.Tasks.Task;

namespace Onion.Application.Mappers
{
    public class TaskViewModelMapper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TaskViewModelMapper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IEnumerable<TaskViewModel> ConstructFromListOfEntities(IEnumerable<Task> tasks)
        {
            var tasksViewModel = tasks.Select(i => new TaskViewModel
            {
                Id = i.TaskId.ToGuid().ToString(),
                Description = i.Description.ToString(),
                Summary = i.Summary.ToString()
            }
            );

            return tasksViewModel;
        }

        public TaskViewModel ConstructFromEntity(Task task)
        {
            return new TaskViewModel
            {
                Id = task.TaskId.ToGuid().ToString(),
                Description = task.Description.ToString(),
                Summary = task.Summary.ToString(),
            };
        }

        public CreateNewTaskCommand ConvertToNewTaskCommand(TaskViewModel taskViewModel)
        {
            return new CreateNewTaskCommand(taskViewModel.Summary, taskViewModel.Description);
        }

        public DeleteTaskCommand ConvertToDeleteTaskCommand(Guid id)
        {
            return new DeleteTaskCommand(id);
        }
    }
}
