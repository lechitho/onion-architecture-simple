using Onion.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Application.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskViewModel>> GetAll();
        Task<TaskViewModel> GetById(Guid id);
        Task<TaskViewModel> Create(TaskViewModel taskViewModel);
        Task Delete(Guid id);
    }
}
