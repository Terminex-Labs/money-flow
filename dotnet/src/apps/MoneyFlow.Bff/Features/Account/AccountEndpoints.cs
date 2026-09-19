using Microsoft.AspNetCore.Mvc;
using MoneyFlow.Bff.Services;
using Shared.Api.Extensions;
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
                    HttpContext httpContext, 
                    [FromBody] Models.CreateAccountRequest request,
                    [FromServices] IAccountClient accountClient, 
                    [FromServices] IJwtReader jwtReader, 
                    [FromServices] ILogger<Program> logger,
                    CancellationToken ct = default
                ) =>
            {
                var accessToken = httpContext.Items["AccessToken"] as string;
                var dto = jwtReader.Extract(accessToken!);

                var requestCreate = new CreateAccountRequest(dto.UserId, request.Name, request.TypeAccountId, request.CurrencyId, request.Balance, request.IsActive);

                var result = await accountClient.CreateAsync(requestCreate, ct);

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
                    HttpContext httpContext, 
                    [FromServices] IAccountClient accountClient, 
                    [FromServices] IJwtReader jwtReader, 
                    [FromServices] ILogger<Program> logger,
                    CancellationToken ct = default
                ) =>
            {
                var accessToken = httpContext.Items["AccessToken"] as string;
                var dto = jwtReader.Extract(accessToken!);

                var result = await accountClient.GetAllAsync(ct);

                return result.Match
                (
                    onSuccess: () => Results.Ok(result.Value),
                    onFailure: errors =>
                    {
                        logger.LogError("Во время выполнения `AccountEndpoints` в `api/v1/account/get`, пришел не удачный ответ от `IAccountClient` в методе `GetAllAsync`! Ошибка: {Errors}", result.StringMessage);
                        return errors.MapToMinimalApiResult();
                    }
                );
            }).RequireAuthorization();

            app.MapPatch($"{_url}/name", async 
                (
                    HttpContext httpContext, 
                    [FromBody] Models.UpdateAccountNameRequest request,
                    [FromServices] IAccountClient accountClient, 
                    [FromServices] IJwtReader jwtReader, 
                    [FromServices] ILogger<Program> logger,
                    CancellationToken ct = default
                ) =>
            {
                var accessToken = httpContext.Items["AccessToken"] as string;
                var dto = jwtReader.Extract(accessToken!);

                var requestUpdate = new UpdateAccountNameRequest(dto.UserId, request.Name);

                var result = await accountClient.UpdateNameAsync(requestUpdate, ct);

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
                    HttpContext httpContext, 
                    [FromRoute] Guid id,
                    [FromServices] IAccountClient accountClient, 
                    [FromServices] IJwtReader jwtReader, 
                    [FromServices] ILogger<Program> logger,
                    CancellationToken ct = default
                ) =>
            {
                var accessToken = httpContext.Items["AccessToken"] as string;
                var dto = jwtReader.Extract(accessToken!);

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
                    HttpContext httpContext, 
                    [FromRoute] Guid id,
                    [FromServices] IAccountClient accountClient, 
                    [FromServices] IJwtReader jwtReader, 
                    [FromServices] ILogger<Program> logger,
                    CancellationToken ct = default
                ) =>
            {
                var accessToken = httpContext.Items["AccessToken"] as string;
                var dto = jwtReader.Extract(accessToken!);

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
                    HttpContext httpContext, 
                    [FromRoute] Guid id,
                    [FromServices] IAccountClient accountClient, 
                    [FromServices] IJwtReader jwtReader, 
                    [FromServices] ILogger<Program> logger,
                    CancellationToken ct = default
                ) =>
            {
                var accessToken = httpContext.Items["AccessToken"] as string;
                var dto = jwtReader.Extract(accessToken!);

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