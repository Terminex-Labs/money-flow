using Shared.Api.Extensions;
using Microsoft.AspNetCore.Mvc;
using Shared.Client.Abstraction;
using Shared.Catalog.Contracts.Request;

namespace MoneyFlow.Bff.Features.TypeTransaction
{
    public static class TypeTransactionEndpoints
    {
        private static readonly string _url = "api/v1/type/transaction";

        public static void MapTypeTransaction(this IEndpointRouteBuilder app)
        {
            app.MapPost(_url, async 
                (
                    [FromBody] CreateTypeTransactionRequest request,
                    [FromServices] ITypeTransactionClient typeTransactionClient, 
                    [FromServices] ILogger<Program> logger,
                    CancellationToken ct = default
                ) =>
            {
                var result = await typeTransactionClient.CreateAsync(request, ct);

                return result.Match
                (
                    onSuccess: () => Results.Ok(result.Value),
                    onFailure: errors =>
                    {
                        logger.LogError("Во время выполнения `TypeTransactionEndpoints` в `api/v1/type/transaction/post`, пришел не удачный ответ от `ITypeTransactionClient` в методе `CreateAsync`! Ошибка: {Errors}", result.StringMessage);
                        return errors.MapToMinimalApiResult();
                    }
                );
            }).RequireAuthorization();

            app.MapGet(_url, async 
                (
                    [FromServices] ITypeTransactionClient typeTransactionClient, 
                    [FromServices] ILogger<Program> logger,
                    CancellationToken ct = default
                ) =>
            {
                var result = await typeTransactionClient.GetAllAsync(ct);

                return result.Match
                (
                    onSuccess: () => Results.Ok(result.Value),
                    onFailure: errors =>
                    {
                        logger.LogError("Во время выполнения `TypeTransactionEndpoints` в `api/v1/type/transaction/get`, пришел не удачный ответ от `ITypeTransactionClient` в методе `GetAllAsync`! Ошибка: {Errors}", result.StringMessage);
                        return errors.MapToMinimalApiResult();
                    }
                );
            }).RequireAuthorization();

            app.MapGet("api/v1/type/transaction/{id}", async 
                (
                    [FromRoute] Guid id, 
                    [FromServices] ITypeTransactionClient typeTransactionClient, 
                    [FromServices] ILogger<Program> logger,
                    CancellationToken ct = default
                ) =>
            {
                var result = await typeTransactionClient.GetByIdAsync(id, ct);

                return result.Match
                (
                    onSuccess: () => Results.Ok(result.Value),
                    onFailure: errors =>
                    {
                        logger.LogError("Во время выполнения `TypeTransactionEndpoints` в `api/v1/type/transaction/get`, пришел не удачный ответ от `ITypeTransactionClient` в методе `GetByIdAsync`! Ошибка: {Errors}", result.StringMessage);
                        return errors.MapToMinimalApiResult();
                    }
                );
            }).RequireAuthorization();

            app.MapPatch(_url, async 
                (
                    [FromBody] UpdateTypeTransactionRequest request,
                    [FromServices] ITypeTransactionClient typeTransactionClient, 
                    [FromServices] ILogger<Program> logger,
                    CancellationToken ct = default
                ) =>
            {
                var result = await typeTransactionClient.UpdateAsync(request, ct);

                return result.Match
                (
                    onSuccess: () => Results.Ok(result.Value),
                    onFailure: errors =>
                    {
                        logger.LogError("Во время выполнения `TypeTransactionEndpoints` в `api/v1/type/transaction/patch`, пришел не удачный ответ от `ITypeTransactionClient` в методе `UpdateAsync`! Ошибка: {Errors}", result.StringMessage);
                        return errors.MapToMinimalApiResult();
                    }
                );
            }).RequireAuthorization();

            app.MapDelete("api/v1/type/transaction/{id}", async 
                (
                    [FromRoute] Guid id,
                    [FromServices] ITypeTransactionClient typeTransactionClient, 
                    [FromServices] ILogger<Program> logger,
                    CancellationToken ct = default
                ) =>
            {
                var result = await typeTransactionClient.DeleteAsync(id, ct);

                return result.Match
                (
                    onSuccess: () => Results.Ok(result.Value),
                    onFailure: errors =>
                    {
                        logger.LogError("Во время выполнения `TypeTransactionEndpoints` в `api/v1/type/transaction/delete`, пришел не удачный ответ от `ITypeTransactionClient` в методе `DeleteAsync`! Ошибка: {Errors}", result.StringMessage);
                        return errors.MapToMinimalApiResult();
                    }
                );
            }).RequireAuthorization();
        }
    }
}