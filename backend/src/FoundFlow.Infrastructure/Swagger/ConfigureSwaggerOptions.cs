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
using Swashbuckle.AspNetCore.Filters;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using System.Collections.Generic;

namespace FoundFlow.Infrastructure.Swagger;

[ExcludeFromCodeCoverage]
public class ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider, IOptions<DocumentationSettings> settings)
    : IConfigureOptions<SwaggerGenOptions>
{
    private readonly DocumentationSettings _settings = settings.Value;

    public void Configure(SwaggerGenOptions options)
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "Header de autorização utilizando JWT. <u>Exemplo:</u> \"Authorization: Bearer {token}\"",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }
        });

        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
        }

        foreach (string filePath in
                 Directory.GetFiles(
                     Path.Combine(
                         Path.GetDirectoryName(
                             Assembly.GetExecutingAssembly().Location) ?? string.Empty),
                     "*.xml"))
        {
            options.IncludeXmlComments(filePath);
        }

        options.ExampleFilters();
        options.EnableAnnotations();

        options.MapType<DateOnly>(() => new OpenApiSchema { Type = "string", Format = "date" });
    }

    private OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        ArgumentNullException.ThrowIfNull(_settings);

        var info = new OpenApiInfo()
        {
            Title = _settings.ApiName,
            Description = _settings.ApiDescription,
            Version = description.ApiVersion.ToString(),
            Contact = new OpenApiContact()
            {
                Name = _settings.ContactName,
                Url = new Uri(_settings.ContactUrl)
            }
        };
        return info;
    }
}