using Shared.Api.Extensions;
using MoneyFlow.Bff.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Client.Abstraction;
using Shared.Catalog.Contracts.Request;

namespace MoneyFlow.Bff.Features.TypeAccount
{
    public static class TypeAccountEndpoints
    {
        private static readonly string _url = "api/v1/type/account";

        public static void MapCurrency(this IEndpointRouteBuilder app)
        {
            app.MapPost(_url, async 
                (
                    HttpContext httpContext, 
                    [FromBody] CreateTypeAccountRequest request,
                    [FromServices] ITypeAccountClient typeAccountClient, 
                    [FromServices] IJwtReader jwtReader, 
                    [FromServices] ILogger<Program> logger,
                    CancellationToken ct = default
                ) =>
            {
                var accessToken = httpContext.Items["AccessToken"] as string;
                var dto = jwtReader.Extract(accessToken!);

                var result = await typeAccountClient.CreateAsync(request, ct);

                return result.Match
                (
                    onSuccess: () => Results.Ok(result.Value),
                    onFailure: errors =>
                    {
                        logger.LogError("Во время выполнения `TypeAccountEndpoints` в `api/v1/type/account/post`, пришел не удачный ответ от `ITypeAccountClient` в методе `CreateAsync`! Ошибка: {Errors}", result.StringMessage);
                        return errors.MapToMinimalApiResult();
                    }
                );
            }).RequireAuthorization();

            app.MapGet(_url, async 
                (
                    HttpContext httpContext, 
                    [FromServices] ITypeAccountClient typeAccountClient, 
                    [FromServices] IJwtReader jwtReader, 
                    [FromServices] ILogger<Program> logger,
                    CancellationToken ct = default
                ) =>
            {
                var accessToken = httpContext.Items["AccessToken"] as string;
                var dto = jwtReader.Extract(accessToken!);

                var result = await typeAccountClient.GetAllAsync(ct);

                return result.Match
                (
                    onSuccess: () => Results.Ok(result.Value),
                    onFailure: errors =>
                    {
                        logger.LogError("Во время выполнения `TypeAccountEndpoints` в `api/v1/type/account/get`, пришел не удачный ответ от `ITypeAccountClient` в методе `GetAllAsync`! Ошибка: {Errors}", result.StringMessage);
                        return errors.MapToMinimalApiResult();
                    }
                );
            }).RequireAuthorization();

            app.MapPatch(_url, async 
                (
                    HttpContext httpContext, 
                    [FromBody] UpdateTypeAccountRequest request,
                    [FromServices] ITypeAccountClient typeAccountClient, 
                    [FromServices] IJwtReader jwtReader, 
                    [FromServices] ILogger<Program> logger,
                    CancellationToken ct = default
                ) =>
            {
                var accessToken = httpContext.Items["AccessToken"] as string;
                var dto = jwtReader.Extract(accessToken!);

                var result = await typeAccountClient.UpdateAsync(request, ct);

                return result.Match
                (
                    onSuccess: () => Results.Ok(result.Value),
                    onFailure: errors =>
                    {
                        logger.LogError("Во время выполнения `TypeAccountEndpoints` в `api/v1/type/account/patch`, пришел не удачный ответ от `ITypeAccountClient` в методе `UpdateAsync`! Ошибка: {Errors}", result.StringMessage);
                        return errors.MapToMinimalApiResult();
                    }
                );
            }).RequireAuthorization();

            app.MapDelete("api/v1/currency/{id}", async 
                (
                    HttpContext httpContext, 
                    [FromRoute] Guid id,
                    [FromServices] ITypeAccountClient currencyClient, 
                    [FromServices] IJwtReader jwtReader, 
                    [FromServices] ILogger<Program> logger,
                    CancellationToken ct = default
                ) =>
            {
                var accessToken = httpContext.Items["AccessToken"] as string;
                var dto = jwtReader.Extract(accessToken!);

                var result = await currencyClient.DeleteAsync(id, ct);

                return result.Match
                (
                    onSuccess: () => Results.Ok(result.Value),
                    onFailure: errors =>
                    {
                        logger.LogError("Во время выполнения `TypeAccountEndpoints` в `api/v1/type/account/patch`, пришел не удачный ответ от `ITypeAccountClient` в методе `DeleteAsync`! Ошибка: {Errors}", result.StringMessage);
                        return errors.MapToMinimalApiResult();
                    }
                );
            }).RequireAuthorization();
        }
    }
}