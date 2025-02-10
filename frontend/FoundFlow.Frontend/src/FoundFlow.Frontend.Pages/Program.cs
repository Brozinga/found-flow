using FoundFlow.Frontend.Core.Configuration;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddStates();

await builder.Build().RunAsync();
