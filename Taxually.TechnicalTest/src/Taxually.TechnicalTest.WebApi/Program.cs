using MediatR;
using System.Net.Http;
using System.Reflection;
using Taxually.TechnicalTest.Core;
using Taxually.TechnicalTest.Core.Interfaces;
using Taxually.TechnicalTest.Core.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddTransient<IHttpClient, TaxuallyHttpClient>();
builder.Services.AddTransient<IQueueClient, TaxuallyQueueClient>();

builder.Services.AddTechnicalTestCoreAssemblyForMediatR();
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

app.MapControllers();

app.Run();
