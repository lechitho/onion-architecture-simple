using Jaeger.Samplers;
using Jaeger;
using Onion.Application.Handlers;
using Onion.Application.Mappers;
using Onion.Application.Services;
using Onion.Application.ViewModels;
using Onion.Domain.Tasks;
using Onion.Infrastructure.Factories;
using Onion.Infrastructure.Repositories;
using OpenTracing.Util;
using OpenTracing;
using System.Reflection;
using FluentMediator;
using Onion.Domain.Tasks.Commands;
using Onion.Domain.Tasks.Events;
using Serilog;
using Onion.API.Extensions.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddTransient<ITaskRepository, TaskRepository>();
builder.Services.AddSingleton<TaskViewModelMapper>();
builder.Services.AddTransient<ITaskFactory, EntityFactory>();

builder.Services.AddScoped<TaskCommandHandler>();
builder.Services.AddScoped<TaskEventHandler>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddFluentMediator(builder =>
{
    builder.On<CreateNewTaskCommand>().PipelineAsync().Return<Onion.Domain.Tasks.Task, TaskCommandHandler>((handler, request) => handler.HandleNewTask(request));

    builder.On<TaskCreatedEvent>().PipelineAsync().Call<TaskEventHandler>((handler, request) => handler.HandleTaskCreatedEvent(request));

    builder.On<DeleteTaskCommand>().PipelineAsync().Call<TaskCommandHandler>((handler, request) => handler.HandleDeleteTask(request));

    builder.On<TaskDeletedEvent>().PipelineAsync().Call<TaskEventHandler>((handler, request) => handler.HandleTaskDeletedEvent(request));
});

builder.Services.AddSingleton(serviceProvider =>
{
    var serviceName = Assembly.GetEntryAssembly().GetName().Name;

    var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

    ISampler sampler = new ConstSampler(true);

    ITracer tracer = new Tracer.Builder(serviceName)
        .WithLoggerFactory(loggerFactory)
        .WithSampler(sampler)
        .Build();

    GlobalTracer.Register(tracer);

    return tracer;
});

Log.Logger = new LoggerConfiguration().CreateLogger();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
