using Azure.Core.GeoJson;
using Dapr;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using OpenroomDapr.Server.Services;
using OpenroomDapr.Shared;
using OpenroomDapr.Shared.Model;
using Serilog;
using System.Text.Json.Serialization;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddTransient<IPersonDataAccess, PersonDataAccess>((services) =>
{
    return new PersonDataAccess(
        services.GetRequiredService<IConfiguration>().GetConnectionString("Default")!,
        services.GetRequiredService<ILogger<PersonDataAccess>>());
});
builder.Services.AddTransient(service => new PersonDataService(
    new PersonDataAccess(
        service.GetRequiredService<IConfiguration>().GetConnectionString("Default")!,
        service.GetRequiredService<ILogger<PersonDataAccess>>())));
builder.Services.AddTransient(service => new FirstPartyAnalyticsService());
builder.Services.AddTransient(service => new AppState());

builder.Host.UseSerilog((hostContext, services, configuration) =>
{
    _ = configuration.ReadFrom.Configuration(hostContext.Configuration);
});
builder.Services.AddDaprSidekick();
builder.Services.AddDaprClient();
WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    _ = app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    _ = app.UseHsts();
}

app.MapSubscribeHandler();

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.MapGet("/numberpublisher", async (int input) =>
{
    using var client = new Dapr.Client.DaprClientBuilder().Build();
    await client.PublishEventAsync("pubsub", "mynumbers", input);
    return Results.Ok(input);
});

app.MapGet("/numbersubscriber", [Topic("pubsub", "mynumbers")] (int input) =>
{
    return Results.Ok(input);
});

app.Run();
