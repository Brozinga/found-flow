var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.FoundFlow_Frontend_Base>("foundflow-frontend-base");

builder.Build().Run();
