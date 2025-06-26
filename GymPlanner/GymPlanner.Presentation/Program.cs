using GymPlanner.Presentation.Components;
using GymPlanner.Infrastructure;
using GymPlanner.Presentation.Services;
using GymPlanner.Common.CQRS;
using GymPlanner.Presentation.Dispatchers;
using GymPlanner.Application;
using GymPlanner.Domain;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


builder.Services.AddCascadingAuthenticationState();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddDomainServices();
builder.Services.AddApplicationServices();

builder.Services.AddScoped<BreadcrumbService>();
builder.Services.AddScoped<ICommandDispatcher, CommandDispatcher>();
builder.Services.AddScoped<IQueryDispatcher, QueryDispatcher>();


builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("es-ES");
    options.SupportedCultures = [new CultureInfo("es-ES")];
    options.SupportedUICultures = [new CultureInfo("es-ES")];
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.Run();

