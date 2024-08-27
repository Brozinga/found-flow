var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.FoundFlow_WebApi>("foundflow-webapi");

builder.Build().Run();
