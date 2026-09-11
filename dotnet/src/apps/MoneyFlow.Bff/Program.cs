using Shared.Redis;
using System.Reflection;
using MoneyFlow.Bff.Extensions;

var builder = WebApplication.CreateBuilder(args);

var assembly = Assembly.GetExecutingAssembly();
var configuration = builder.Configuration;
var env = builder.Environment;

builder.Services.AddOpenApi()
    .AddAuthorization()
    .UseHttpClient(configuration)
    .UseDistributedLock()
    .UseServices()
    .UseCache(configuration)
    .UseSharedCryptoKeyASPNET(configuration)
    .UseConfigureOptions()
    .UseCors()
    .UseCookie(env);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowSpecificOrigin");
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(assembly);

app.Run();
