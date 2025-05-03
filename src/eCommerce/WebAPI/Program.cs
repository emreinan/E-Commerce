using Application;
using Infrastructure;
using Persistence;
using Scalar.AspNetCore;
using Core.CrossCuttingConcerns.Exceptions.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices();

var app = builder.Build();

app.ConfigureCustomExceptionMiddleware();

app.MapOpenApi();
app.MapScalarApiReference();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
