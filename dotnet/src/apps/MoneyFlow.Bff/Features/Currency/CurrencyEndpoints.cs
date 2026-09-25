using Shared.Api.Extensions;
using Microsoft.AspNetCore.Mvc;
using Shared.Client.Abstraction;
using Shared.Catalog.Contracts.Request;

namespace MoneyFlow.Bff.Features.Currency
{
    public static class CurrencyEndpoints
    {
        private static readonly string _url = "api/v1/currency";

        public static void MapCurrency(this IEndpointRouteBuilder app)
        {
            app.MapPost(_url, async 
                (
                    [FromBody] CreateCurrencyRequest request,
                    [FromServices] ICurrencyClient currencyClient, 
                    [FromServices] ILogger<Program> logger,
                    CancellationToken ct = default
                ) =>
            {
                var result = await currencyClient.CreateAsync(request, ct);

                return result.Match
                (
                    onSuccess: () => Results.Ok(result.Value),
                    onFailure: errors =>
                    {
                        logger.LogError("Во время выполнения `CurrencyEndpoints` в `api/v1/currency/post`, пришел не удачный ответ от `ICurrencyClient` в методе `CreateAsync`! Ошибка: {Errors}", result.StringMessage);
                        return errors.MapToMinimalApiResult();
                    }
                );
            }).RequireAuthorization();

            app.MapGet(_url, async 
                (
                    [FromServices] ICurrencyClient currencyClient, 
                    [FromServices] ILogger<Program> logger,
                    CancellationToken ct = default
                ) =>
            {
                var result = await currencyClient.GetAllAsync(ct);

                return result.Match
                (
                    onSuccess: () => Results.Ok(result.Value),
                    onFailure: errors =>
                    {
                        logger.LogError("Во время выполнения `CurrencyEndpoints` в `api/v1/currency/get`, пришел не удачный ответ от `ICurrencyClient` в методе `GetAllAsync`! Ошибка: {Errors}", result.StringMessage);
                        return errors.MapToMinimalApiResult();
                    }
                );
            }).RequireAuthorization();

            app.MapGet("api/v1/currency/{id}", async 
                (
                    [FromRoute] Guid id,
                    [FromServices] ICurrencyClient currencyClient, 
                    [FromServices] ILogger<Program> logger,
                    CancellationToken ct = default
                ) =>
            {
                var result = await currencyClient.GetByIdAsync(id, ct);

                return result.Match
                (
                    onSuccess: () => Results.Ok(result.Value),
                    onFailure: errors =>
                    {
                        logger.LogError("Во время выполнения `CurrencyEndpoints` в `api/v1/currency/get`, пришел не удачный ответ от `ICurrencyClient` в методе `GetByIdAsync`! Ошибка: {Errors}", result.StringMessage);
                        return errors.MapToMinimalApiResult();
                    }
                );
            }).RequireAuthorization();

            app.MapPatch(_url, async 
                (
                    [FromBody] UpdateCurrencyRequest request,
                    [FromServices] ICurrencyClient currencyClient, 
                    [FromServices] ILogger<Program> logger,
                    CancellationToken ct = default
                ) =>
            {
                var result = await currencyClient.UpdateAsync(request, ct);

                return result.Match
                (
                    onSuccess: () => Results.Ok(result.Value),
                    onFailure: errors =>
                    {
                        logger.LogError("Во время выполнения `CurrencyEndpoints` в `api/v1/currency/patch`, пришел не удачный ответ от `ICurrencyClient` в методе `UpdateAsync`! Ошибка: {Errors}", result.StringMessage);
                        return errors.MapToMinimalApiResult();
                    }
                );
            }).RequireAuthorization();

            app.MapDelete("api/v1/currency/{id}", async 
                (
                    [FromRoute] Guid id,
                    [FromServices] ICurrencyClient currencyClient, 
                    [FromServices] ILogger<Program> logger,
                    CancellationToken ct = default
                ) =>
            {
                var result = await currencyClient.DeleteAsync(id, ct);

                return result.Match
                (
                    onSuccess: () => Results.Ok(result.Value),
                    onFailure: errors =>
                    {
                        logger.LogError("Во время выполнения `CurrencyEndpoints` в `api/v1/currency/delete`, пришел не удачный ответ от `ICurrencyClient` в методе `DeleteAsync`! Ошибка: {Errors}", result.StringMessage);
                        return errors.MapToMinimalApiResult();
                    }
                );
            }).RequireAuthorization();
        }
    }
}