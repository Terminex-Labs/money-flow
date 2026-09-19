using Serilog;
using Shared.Logging;
using Shared.Api.Extensions;
using MoneyFlow.Catalog.Application.Extensions;
using MoneyFlow.Catalog.Infrastructure.Extensions;
using MoneyFlow.Catalog.Api.Constants;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

builder.Host.AddSerilogLogger();
builder.Services.AddControllers();

builder.Services
    .AddOpenApi()
    .UseAuthentication(configuration)
    .AddAuthorization(options => options.AddPolicy(AuthorizationPolicyConstants.ADMIN_ONLY, policy => policy.RequireRole("Admin")))
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
app.UseRouting();
app.UseSerilogRequestLogging();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Logger.LogInformation("Приложение успешно запущено и готово к работе!");

app.Run();