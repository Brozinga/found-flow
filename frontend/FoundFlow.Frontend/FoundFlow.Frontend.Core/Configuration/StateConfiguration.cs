using FoundFlow.Frontend.Core.States;
using Microsoft.Extensions.DependencyInjection;

namespace FoundFlow.Frontend.Core.Configuration;

public static class StateConfiguration
{
    public static void AddStates(this IServiceCollection service)
    {
        service.AddScoped<CategoryState>();
    }
}