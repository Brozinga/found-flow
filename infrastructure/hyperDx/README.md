# Modo de usar

Para usar o HyperDX no projeto .NET Core, você precisa adicionar o OpenTelemetry no projeto, e os seguintes pontos:

1- Configurar no `appsettings.json`

```json
  "OpenTelemetrySettings": {
    "Endpoint": "http://localhost:4318/v1/traces",
    "Application": "application-name",
    "Header": "authorization=<TOKEN_DENTRO_HYPERDX>"
  },
```

2- Adicione ao service a leitura do OpenTelemetry

```csharp

  private static void AddOpenTelemetry(WebApplicationBuilder builder)
    {
        OpenTelemetrySettings openTelemetrySettings =
            builder.Configuration.GetValueOrThrow<OpenTelemetrySettings>("OpenTelemetrySettings");

        Action<ResourceBuilder> configureResource = r => r.AddService(
            serviceName: openTelemetrySettings.Application!,
            serviceVersion: typeof(Program).Assembly.GetName().Version?.ToString() ?? "unknown",
            serviceInstanceId: Environment.MachineName);

        builder.Services.AddOpenTelemetry()
            .ConfigureResource(resource => { resource.AddService(openTelemetrySettings.Application!); })
            .WithTracing(tracing => tracing
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddOtlpExporter(o =>
                {
                    o.Endpoint = new Uri(openTelemetrySettings.Endpoint!);
                    o.Protocol = OtlpExportProtocol.HttpProtobuf;
                    o.Headers = openTelemetrySettings.Header;
                }))
            .WithMetrics(metrics => metrics
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddOtlpExporter(o =>
                {
                    o.Endpoint = new Uri(openTelemetrySettings.Endpoint!);
                    o.Protocol = OtlpExportProtocol.HttpProtobuf;
                    o.Headers = openTelemetrySettings.Header;
                }));

        builder.Logging.AddOpenTelemetry(options =>
        {
            ResourceBuilder resourceBuilder = ResourceBuilder.CreateDefault();
            configureResource(resourceBuilder);
            options.SetResourceBuilder(resourceBuilder);
        });
    }

```

E pronto o HyperDX já vai ter o necessário.
