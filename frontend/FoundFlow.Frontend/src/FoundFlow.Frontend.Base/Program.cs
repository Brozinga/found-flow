using System.Globalization;
using FoundFlow.Frontend.Base.Components;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddLocalization();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

var app = builder.Build();

app.UseRequestLocalization(new RequestLocalizationOptions()
    .AddSupportedCultures("pt-BR")
    .AddSupportedUICultures("pt-BR"));

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(FoundFlow.Frontend.Pages._Imports).Assembly);

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");

await app.RunAsync();
