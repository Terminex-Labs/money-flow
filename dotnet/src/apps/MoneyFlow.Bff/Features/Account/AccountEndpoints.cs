using Shared.Api.Extensions;
using MoneyFlow.Bff.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Client.Abstraction;
using Shared.Ledger.Contracts.Accounts.Request;

namespace MoneyFlow.Bff.Features.Account
{
    public static class AccountEndpoints
    {
        private static readonly string _url = "api/v1/account";

        public static void MapCurrency(this IEndpointRouteBuilder app)
        {
            app.MapPost(_url, async 
                (
                    [FromBody] CreateAccountRequest request,
                    [FromServices] IAccountClient accountClient, 
                    [FromServices] IJwtReader jwtReader, 
                    [FromServices] ILogger<Program> logger,
                    CancellationToken ct = default
                ) =>
            {
                var result = await accountClient.CreateAsync(request, ct);

                return result.Match
                (
                    onSuccess: () => Results.Ok(result.Value),
                    onFailure: errors =>
                    {
                        logger.LogError("Во время выполнения `AccountEndpoints` в `api/v1/account/post`, пришел не удачный ответ от `IAccountClient` в методе `CreateAsync`! Ошибка: {Errors}", result.StringMessage);
                        return errors.MapToMinimalApiResult();
                    }
                );
            }).RequireAuthorization();

            app.MapGet(_url, async 
                (
                    [FromServices] IAccountClient accountClient, 
                    [FromServices] ICurrencyClient currencyClient,
                    [FromServices] ITypeAccountClient typeAccountClient,
                    [FromServices] IJwtReader jwtReader, 
                    [FromServices] ILogger<Program> logger,
                    CancellationToken ct = default
                ) =>
            {
                var resultAccount = await accountClient.GetAllAsync(ct);
                var resultCurrency = await currencyClient.GetAllAsync(ct);
                var resultTypeAccount = await typeAccountClient.GetAllAsync(ct);

                var response = resultAccount.Value.Select
                (
                    account =>
                    {
                        var currency = resultCurrency.Value.FirstOrDefault(currency => currency.Id == account.CurrencyId);
                        var typeAccountName = resultTypeAccount.Value.FirstOrDefault(typeAccount => typeAccount.Id == account.TypeAccountId)?.Name;

                        return new Models.AccountResponse
                        (
                            account.Id.ToString(), 
                            account.Name, 
                            currency == null ? null : new Models.AccountDataCurrencyResponse(currency.Id.ToString(), currency.ShortName, currency.Unicode, currency.FullName),
                            typeAccountName,
                            account.Balance,
                            account.IsActive
                        );
                    } 
                );

                return resultAccount.Match
                (
                    onSuccess: () => Results.Ok(response),
                    onFailure: errors =>
                    {
                        logger.LogError("Во время выполнения `AccountEndpoints` в `api/v1/account/get`, пришел не удачный ответ от `IAccountClient` в методе `GetAllAsync`! Ошибка: {Errors}", resultAccount.StringMessage);
                        return errors.MapToMinimalApiResult();
                    }
                );
            }).RequireAuthorization();

            app.MapPatch($"{_url}/name", async 
                (
                    [FromBody] UpdateAccountNameRequest request,
                    [FromServices] IAccountClient accountClient, 
                    [FromServices] IJwtReader jwtReader, 
                    [FromServices] ILogger<Program> logger,
                    CancellationToken ct = default
                ) =>
            {
                var result = await accountClient.UpdateNameAsync(request, ct);

                return result.Match
                (
                    onSuccess: () => Results.Ok(result.Value),
                    onFailure: errors =>
                    {
                        logger.LogError("Во время выполнения `AccountEndpoints` в `api/v1/account/patch`, пришел не удачный ответ от `IAccountClient` в методе `UpdateNameAsync`! Ошибка: {Errors}", result.StringMessage);
                        return errors.MapToMinimalApiResult();
                    }
                );
            }).RequireAuthorization();

            app.MapPatch("api/v1/account/freeze/{id}", async 
                (
                    [FromRoute] Guid id,
                    [FromServices] IAccountClient accountClient, 
                    [FromServices] IJwtReader jwtReader, 
                    [FromServices] ILogger<Program> logger,
                    CancellationToken ct = default
                ) =>
            {
                var result = await accountClient.FreezeAsync(id, ct);

                return result.Match
                (
                    onSuccess: () => Results.Ok(result.Value),
                    onFailure: errors =>
                    {
                        logger.LogError("Во время выполнения `AccountEndpoints` в `api/v1/account/patch`, пришел не удачный ответ от `IAccountClient` в методе `FreezeAsync`! Ошибка: {Errors}", result.StringMessage);
                        return errors.MapToMinimalApiResult();
                    }
                );
            }).RequireAuthorization();

            app.MapPatch("api/v1/account/unfreeze/{id}", async 
                (
                    [FromRoute] Guid id,
                    [FromServices] IAccountClient accountClient, 
                    [FromServices] IJwtReader jwtReader, 
                    [FromServices] ILogger<Program> logger,
                    CancellationToken ct = default
                ) =>
            {
                var result = await accountClient.UnfreezeAsync(id, ct);

                return result.Match
                (
                    onSuccess: () => Results.Ok(result.Value),
                    onFailure: errors =>
                    {
                        logger.LogError("Во время выполнения `AccountEndpoints` в `api/v1/account/patch`, пришел не удачный ответ от `IAccountClient` в методе `UnfreezeAsync`! Ошибка: {Errors}", result.StringMessage);
                        return errors.MapToMinimalApiResult();
                    }
                );
            }).RequireAuthorization();

            app.MapDelete("api/v1/account/{id}", async 
                (
                    [FromRoute] Guid id,
                    [FromServices] IAccountClient accountClient, 
                    [FromServices] IJwtReader jwtReader, 
                    [FromServices] ILogger<Program> logger,
                    CancellationToken ct = default
                ) =>
            {
                var result = await accountClient.DeleteAsync(id, ct);

                return result.Match
                (
                    onSuccess: () => Results.Ok(result.Value),
                    onFailure: errors =>
                    {
                        logger.LogError("Во время выполнения `AccountEndpoints` в `api/v1/account/delete`, пришел не удачный ответ от `IAccountClient` в методе `DeleteAsync`! Ошибка: {Errors}", result.StringMessage);
                        return errors.MapToMinimalApiResult();
                    }
                );
            }).RequireAuthorization();
        }
    }
}