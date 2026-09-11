using Serilog;
using Shared.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddSerilogLogger();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseSerilogRequestLogging();

app.Logger.LogInformation("Приложение успешно запущено и готово к работе!");

app.Run();