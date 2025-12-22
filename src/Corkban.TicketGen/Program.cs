using Corkban.TicketGen;
using Corkban.TicketGen.Entities;
using Corkban.TicketGen.Infrastructure.Repositories;
using Corkban.TicketGen.Infrastructure.Sqlite;
using Corkban.TicketGen.Auth;
using Corkban.TicketGen.Configuration;

var builder = WebApplication.CreateBuilder(args);

// add configuration
builder.Configuration.AddJsonFile("secrets.json", true, true);
builder.Configuration.AddEnvironmentVariables();

builder.Services.Configure<PrinterConfiguration>(builder.Configuration.GetSection(PrinterConfiguration.SectionName));
builder.Services.Configure<DataConfiguration>(builder.Configuration.GetSection(DataConfiguration.SectionName));

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<SqliteContext>();

builder.Services.AddScoped<IRepository<TemplateEntity>, TemplateRepository>();

builder.Services.AddAuthentication(ApiKeyAuthenticationSchemeOptions.DefaultScheme)
    .AddScheme<ApiKeyAuthenticationSchemeOptions, ApiKeyAuthenticationHandler>(
        ApiKeyAuthenticationSchemeOptions.DefaultScheme, _ => { });
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthentication();
app.UseAuthorization();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", [Microsoft.AspNetCore.Authorization.Authorize] async ([Microsoft.AspNetCore.Mvc.FromServices] IRepository<TemplateEntity> templateRepo) =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();

        var foo = await templateRepo.GetAllAsync();

        return foo;
    })
    .WithName("GetWeatherForecast");

app.Run();

namespace Corkban.TicketGen
{
    record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}