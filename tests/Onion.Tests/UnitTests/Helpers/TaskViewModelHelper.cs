using Onion.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Tests.UnitTests.Helpers
{
    public static class TaskViewModelHelper
    {
        public static TaskViewModel GetTaskViewModel()
        {
            return new TaskViewModel
            {
                Id = Guid.NewGuid().ToString(),
                Summary = "Summary",
                Description = "Description"
            };
        }
    }
}
