using Onion.Domain.Tasks.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Application.Handlers
{
    public class TaskEventHandler
    {
        public async Task HandleTaskCreatedEvent(TaskCreatedEvent taskCreatedEvent)
        {
            // Here you can do whatever you need with this event, you can propagate the data using a queue, calling another API or sending a notification or whatever
            // With this scenario, you are building a event driven architecture with microservices and DDD
        }

        public async Task HandleTaskDeletedEvent(TaskDeletedEvent taskDeletedEvent)
        {
            // Here you can do whatever you need with this event, you can propagate the data using a queue, calling another API or sending a notification or whatever
            // With this scenario, you are building a event driven architecture with microservices and DDD
        }
    }
}
