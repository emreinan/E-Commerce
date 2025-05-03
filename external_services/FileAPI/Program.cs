using FileAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<FileDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite"));
});

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

using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<FileDbContext>();
await context.Database.EnsureCreatedAsync();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
