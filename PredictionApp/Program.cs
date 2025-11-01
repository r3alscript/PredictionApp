using Microsoft.EntityFrameworkCore;
using PredictionApp.Infrastructure.Data;
using PredictionApp.Infrastructure;
using PredictionApp.Domain;
using System;
using PredictionApp.Domain.Interfaces;
using PredictionApp.Domain.Services;
using PredictionApp.Middlewares;
using PredictionApp.Filters;
using PredictionApp.Domain.Events;
using PredictionApp.Infrastructure.Events;
using PredictionApp.Infrastructure.Utilities;
using PredictionApp.Domain.Entities;
using PredictionApp.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<PredictionAppDbContext>(options =>
    options.UseSqlite("Data Source=appdata.db"));

builder.Services.AddSingleton<IRandomProvider, RandomProvider>();

builder.Services.AddTransient<IDateTimeProvider, SystemDateTimeProvider>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IRepository<Prediction>, Repository<Prediction>>();
builder.Services.AddScoped<IRepository<Motivation>, Repository<Motivation>>();
builder.Services.AddScoped<IPredictionService, PredictionService>();
builder.Services.AddScoped<IMotivationService, MotivationService>();
builder.Services.AddScoped<IEventHandler<PredictionCreatedEvent>, PredictionCreatedEventHandler>();
builder.Services.AddScoped<IEventHandler<MotivationCreatedEvent>, MotivationCreatedEventHandler>();



builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseGlobalExceptionHandler();

app.UseThrowMiddleware();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseDefaultFiles();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
