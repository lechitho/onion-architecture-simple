﻿namespace Onion.Domain.Tasks.ValueObjects
{
    public struct TaskId
    {
        private readonly Guid _taskId;

        public TaskId(Guid taskId)
        {
            if (taskId.Equals(Guid.Empty))
                throw new ArgumentNullException($"Task Id does not have any value");

            _taskId = taskId;
        }

        public Guid ToGuid()
        {
            return _taskId;
        }
    }
}
