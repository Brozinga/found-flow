var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var webapi = builder.AddProject<Projects.FoundFlow_WebApi>("foundflow-webapi");

builder.AddProject<Projects.FoundFlow_Frontend_Base>("foundflow-frontend")
    .WithReference(webapi);


builder.Build().Run();
