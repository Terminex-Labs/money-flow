using Serilog;
using Shared.Logging;
using System.Reflection;
using MoneyFlow.Ledger.Api.Extensions;
using MoneyFlow.Ledger.Application.Extensions;
using MoneyFlow.Ledger.Infrastructure.Extensions;

var assembly = Assembly.GetExecutingAssembly();
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

builder.Host.AddSerilogLogger();
builder.Services.AddControllers();

builder.Services
    .AddOpenApi()
    .UseMediatR()
    .UseRepository()
    .UsePostgres(configuration)
    .UseDapper(configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseSerilogRequestLogging();
app.MapControllers();
app.UseEndpoints(assembly);

app.Logger.LogInformation("Приложение успешно запущено и готово к работе!");

app.Run();