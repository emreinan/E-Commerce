using Projects;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<WebAPI>("webapi");

builder.Build().Run();
