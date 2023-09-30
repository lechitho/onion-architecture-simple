using FluentMediator;
using Onion.Application.Mappers;
using Onion.Application.ViewModels;
using Onion.Domain.Tasks;
using OpenTracing;

namespace Onion.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskFactory _taskFactory;
        private readonly TaskViewModelMapper _taskViewModelMapper;
        private readonly ITracer _tracer;
        private readonly IMediator _mediator;

        public TaskService(ITaskRepository taskRepository, TaskViewModelMapper taskViewModelMapper, ITracer tracer, ITaskFactory taskFactory, IMediator mediator)
        {
            _taskRepository = taskRepository;
            _taskViewModelMapper = taskViewModelMapper;
            _tracer = tracer;
            _taskFactory = taskFactory;
            _mediator = mediator;
        }

        public async Task<TaskViewModel> Create(TaskViewModel taskViewModel)
        {
            using (var scope = _tracer.BuildSpan("Create_TaskService").StartActive(true))
            {
                var newTaskCommand = _taskViewModelMapper.ConvertToNewTaskCommand(taskViewModel);

                var taskResult = await _mediator.SendAsync<Domain.Tasks.Task>(newTaskCommand);

                return _taskViewModelMapper.ConstructFromEntity(taskResult);
            }
        }

        public async System.Threading.Tasks.Task Delete(Guid id)
        {
            using (var scope = _tracer.BuildSpan("Delete_TaskService").StartActive(true))
            {
                var deleteTaskCommand = _taskViewModelMapper.ConvertToDeleteTaskCommand(id);
                await _mediator.PublishAsync(deleteTaskCommand);
            }
        }

        public async Task<IEnumerable<TaskViewModel>> GetAll()
        {
            using (var scope = _tracer.BuildSpan("GetAll_TaskService").StartActive(true))
            {
                var tasksEntities = await _taskRepository.FindAll();

                return _taskViewModelMapper.ConstructFromListOfEntities(tasksEntities);
            }
        }

        public async Task<TaskViewModel> GetById(Guid id)
        {
            using (var scope = _tracer.BuildSpan("GetById_TaskService").StartActive(true))
            {
                var taskEntity = await _taskRepository.FindById(id);

                return _taskViewModelMapper.ConstructFromEntity(taskEntity);
            }
        }
    }
}
