﻿using FluentMediator;
using Microsoft.AspNetCore.Http;
using Moq;
using Onion.Application.Mappers;
using Onion.Domain.Tasks;
using Onion.Domain.Tasks.Commands;
using OpenTracing;
using OpenTracing.Mock;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Onion.Tests.UnitTests.Application.Services
{
    public class TaskServiceTests
    {
        private readonly Mock<ITaskRepository> _mockTaskRepository = new Mock<ITaskRepository>();
        private readonly Mock<ITaskFactory> _mockTaskFactory = new Mock<ITaskFactory>();
        private readonly Mock<ITracer> _mockITracer = new Mock<ITracer>();
        private readonly Mock<IMediator> _mockIMediator = new Mock<IMediator>();
        private static readonly Mock<IHttpContextAccessor> _mockIHttpContextAccessor = new Mock<IHttpContextAccessor>();

        private readonly TaskViewModelMapper _mockTaskViewModelMapper = new TaskViewModelMapper(_mockIHttpContextAccessor.Object);

        [Fact]
        public async System.Threading.Tasks.Task Create_Success()
        {
            //Arrange
            _mockITracer.Setup(x => x.BuildSpan(It.IsAny<string>())).Returns(() => new MockSpanBuilder(new MockTracer(), ""));
            _mockIMediator.Setup(x => x.SendAsync<Domain.Tasks.Task>(It.IsAny<CreateNewTaskCommand>(), null))
                .Returns(System.Threading.Tasks.Task.FromResult(TaskHelper.GetTask()));
            _mockIHttpContextAccessor.Setup(x => x.HttpContext).Returns(HttpContextHelper.GetHttpContext());

            //Act

            var taskService = new TaskService(_mockTaskRepository.Object, _mockTaskViewModelMapper, _mockITracer.Object, _mockTaskFactory.Object, _mockIMediator.Object);
            var result = await taskService.Create(TaskViewModelHelper.GetTaskViewModel());

            //Assert
            Assert.NotNull(result);

            Assert.Equal("Summary", result.Summary);
            Assert.Equal("Description", result.Description);

            Assert.NotNull(result.Id);
            Assert.NotNull(result.Description);

            _mockITracer.Verify(x => x.BuildSpan(It.IsAny<string>()), Times.Once);
            _mockIMediator.Verify(x => x.SendAsync<Task>(It.IsAny<CreateNewTaskCommand>(), null), Times.Once);
        }
    }
}
