using System;
using AspNetCoreRateLimit;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Asp.Versioning;
using FoundFlow.Domain.Interfaces;
using FoundFlow.Domain.Interfaces.Repositories;
using FoundFlow.Infrastructure.Database;
using FoundFlow.Infrastructure.Database.Queries;
using FoundFlow.Infrastructure.Database.Repositories;
using FoundFlow.Infrastructure.Database.UoW;
using FoundFlow.Infrastructure.Extensions;
using FoundFlow.Infrastructure.Filters;
using FoundFlow.Infrastructure.Managers;
using FoundFlow.Infrastructure.Services;
using FoundFlow.Infrastructure.Swagger;
using FoundFlow.Shared.Settings;
using Hellang.Middleware.ProblemDetails.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using FoundFlow.Shared.Documentation.Examples;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;

namespace FoundFlow.Infrastructure.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddSettings(config);
        services.ConfigureManagerDb();
        services.ConfigureRateLimit(config);
        services.ConfigureDefaultCulture();

        services.RegisterServices();

        services.ConfigureControllers();
        services.ConfigureProblemDetails();
        services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

        services.ConfigureSwagger();
        services.ConfigureDatabase(config);
        services.ConfigureRepositories();

        services.ConfigureAuthentication(config);

        services.ConfigureGraphql();

        services.ConfigureHostPaths();
        services.ConfigureCors(config);
        return services;
    }

    private static void ConfigureRateLimit(this IServiceCollection services, IConfiguration configuration)
    {
        // Rate Limit
        if (configuration.GetSection("IpRateLimitSettings:EnableEndpointRateLimiting").Get<bool>())
        {
            services.AddMemoryCache();
            services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimitSettings"));
            services.Configure<IpRateLimitPolicies>(configuration.GetSection("IpRateLimitPolicies"));
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

            // Caso esteja utilizando load-balance, deve-se alterar as implementações
            // abaixo para usar DistributedCacheIpPolicyStore e DistributedCacheRateLimitCounterStore
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
        }
    }

    private static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.DescribeAllParametersInCamelCase();
        });
        services.AddSwaggerExamples();
        services.AddSwaggerExamplesFromAssemblyOf<ValidationProblemDetailsExample>();
        services.AddSwaggerExamplesFromAssemblyOf<NotFoundProblemDetailsExample>();
        services.AddSwaggerExamplesFromAssemblyOf<BadRequestProblemDetailsExample>();
        services.AddSwaggerExamplesFromAssemblyOf<UnauthorizedProblemDetailsExample>();
        services.AddSwaggerExamplesFromAssemblyOf<ForbiddenProblemDetailsExample>();
        services.AddSwaggerExamplesFromAssemblyOf<InternalServerProblemDetailsExample>();
        services.AddSwaggerExamplesFromAssemblyOf<ServiceUnavailableProblemDetailsExample>();
        services.ConfigureOptions<ConfigureSwaggerOptions>();
        services.AddApiVersioning(option =>
            {
                option.DefaultApiVersion = new ApiVersion(1, 0);
                option.AssumeDefaultVersionWhenUnspecified = true;
                option.ReportApiVersions = true;
                option.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddMvc()
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
    }

    private static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var tokenSettings = configuration.GetValueOrThrow<JwtSettings>("JwtSettings");

        byte[] key = Encoding.ASCII.GetBytes(tokenSettings.Key);
        var credentials = new SymmetricSecurityKey(key);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
            x.IncludeErrorDetails = true;
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = credentials,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = tokenSettings.ValidIssuer,
                ValidAudience = tokenSettings.ValidAudience
            };
        });
    }

    private static void ConfigureHostPaths(this IServiceCollection services)
    {
        services.AddRouting(options => options.LowercaseUrls = true);
        services.AddHealthChecks();
        services.AddMvc(x => x.EnableEndpointRouting = false);
    }

    private static void ConfigureCors(this IServiceCollection services, IConfiguration configuration)
    {
        var corsSettings = configuration.GetValueOrThrow<CorsSettings>("CorsSettings");
        services.AddCors(co =>
            co.AddPolicy("CorsPolicy", cpb =>
                cpb.SetIsOriginAllowed(path => corsSettings.Origin.Count == 0 || corsSettings.Origin.Contains(path))
                    .AllowAnyHeader()
                    .AllowAnyMethod()));
    }

    private static void ConfigureControllers(this IServiceCollection services)
    {
        services.AddControllers(options =>
            {
                options.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
                options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
                options.Filters.Add<ApiExceptionHandlingFilterAttribute>();
            })
            .AddNewtonsoftJson(
                options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
            .AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
    }

    private static void ConfigureProblemDetails(this IServiceCollection services)
    {
        services.AddProblemDetails(options =>
            {
                options.Rethrow<NotSupportedException>();
                options.MapToStatusCode<NotImplementedException>(StatusCodes.Status501NotImplemented);
                options.MapToStatusCode<HttpRequestException>(StatusCodes.Status503ServiceUnavailable);
            })
            .AddProblemDetailsConventions();
    }

    private static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var logsInfo = configuration.GetValueOrThrow<LoggingSettings>("LoggingSettings");
        if (logsInfo.LogDatabase)
        {
            services.AddDbContext<ApplicationDbContext>(op => op.UseNpgsql(
                    configuration.GetConnectionString("ApplicationDatabase"))
                .UseLazyLoadingProxies()
                .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddSerilog())));
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(op => op.UseNpgsql(
                    configuration.GetConnectionString("ApplicationDatabase"))
                .UseLazyLoadingProxies());
        }

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
    }

    private static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<ICategoriesRepository, CategoriesRepository>();
        services.AddScoped<ITransactionsRepository, TransactionsRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void ConfigureManagerDb(this IServiceCollection services)
    {
        services.AddScoped<IManagerService, ManagerService>();
    }

    private static void RegisterServices(this IServiceCollection services)
    {
        services.AddTransient<ITokenService, TokenService>();
    }

    private static void ConfigureGraphql(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .AddAuthorization()
            .AddQueryType<Query>()
            .AddProjections()
            .AddFiltering()
            .AddSorting();
    }

    private static void ConfigureDefaultCulture(this IServiceCollection service)
    {
        service.Configure<RequestLocalizationOptions>(options =>
        {
            options.DefaultRequestCulture = new RequestCulture("pt-BR");
        });
    }
}