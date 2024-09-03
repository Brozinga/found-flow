using Aspire.Hosting;
using Microsoft.Extensions.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var webapi = builder.AddProject<Projects.FoundFlow_WebApi>("foundflow-webapi");

builder.AddProject<Projects.FoundFlow_Frontend_Base>("foundflow-frontend")
    .WithReference(webapi);


await builder.Build().RunAsync();
