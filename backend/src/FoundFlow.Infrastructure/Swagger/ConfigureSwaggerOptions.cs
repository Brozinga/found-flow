using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using Asp.Versioning.ApiExplorer;
using FoundFlow.Shared.Settings;

namespace FoundFlow.Infrastructure.Swagger;

[ExcludeFromCodeCoverage]
public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;
    private readonly SwaggerSettings _settings;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider, IOptions<SwaggerSettings> settings)
    {
        _provider = provider;
        _settings = settings.Value;
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
        }

        options.CustomSchemaIds(type => type.ToString());

        string xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    }

    private OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        var info = new OpenApiInfo()
        {
            Title = _settings.ApiName,
            Description = _settings.ApiDescription,
            Version = description.ApiVersion.ToString(),
            Contact = new OpenApiContact()
            {
                Name = _settings.ContactName,
                Url = _settings.ContactUrl
            }
        };
        return info;
    }
}
