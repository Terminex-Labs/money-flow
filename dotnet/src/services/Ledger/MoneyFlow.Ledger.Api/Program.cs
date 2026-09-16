using Serilog;
using Shared.Logging;
using MoneyFlow.Ledger.Application.Extensions;
using MoneyFlow.Ledger.Infrastructure.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

builder.Host.AddSerilogLogger();

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

app.Logger.LogInformation("Приложение успешно запущено и готово к работе!");

app.Run();