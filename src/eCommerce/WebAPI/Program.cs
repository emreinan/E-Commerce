using Application;
using Core.Application.Jwt;
using Core.CrossCuttingConcerns.Exceptions.Extensions;
using Infrastructure;
using Persistence;
using Persistence.Seeding;
using WebAPI;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

app.ConfigureCustomExceptionMiddleware();

app.UseMiddleware<JwtHeaderFixingMiddleware>();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "ECommerce App");
    options.RoutePrefix = string.Empty; 
});

await DataSeeder.SeedDatabaseAsync(app.Services);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
