using MoneyFlow.Bff.Helpers;
using MoneyFlow.Bff.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Client.Abstraction;
using Shared.Files.Contracts.Request;
using MoneyFlow.Bff.Features.Overview.Models;

namespace MoneyFlow.Bff.Features.Overview
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("user/dashboard", async 
                (
                    HttpContext httpContext, 
                    [FromServices] IIdentityHubClient identityHubClient, 
                    [FromServices] IFileClient fileClient, 
                    [FromServices] IJwtReader jwtReader, 
                    [FromServices] ILogger<Program> logger,
                    CancellationToken ct = default
                ) =>
            {
                var accessToken = httpContext.Items["AccessToken"] as string;
                var dto = jwtReader.Extract(accessToken!);

                var result = await identityHubClient.GetUserInfoAsync(dto.UserId, ct);

                if (result.IsFailure)
                {
                    logger.LogError("Во время выполнения `AuthEndpoints` в `user/dashboard`, пришел не удачный ответ от `IdentityHubService` в методе `GetUserInfoAsync`! Ошибка: {Errors}", result.StringMessage);
                    return Results.StatusCode(StatusCodes.Status500InternalServerError);
                }

                var response = new DashboardResponse(AvatarUrl: null, UserName: result.Value.UserName);

                if (!string.IsNullOrWhiteSpace(result.Value.Avatar))
                {
                    var (bucketName, fileName) = S3UrlParser.Parse(result.Value.Avatar);
                    var s3Result = await fileClient.GetPresignedUrlAsync(new PresignedUrlRequest(bucketName!, fileName!), ct);

                    s3Result.Switch
                    (
                        () => response = response with { AvatarUrl = s3Result.Value.PresignedUrl }, 
                        errors => logger.LogError("Во время выполнения `AuthEndpoints` в `user/dashboard`, пришел не удачный ответ от `MinervaService` в методе `GetPresignedUrlAsync`! Ошибка: {Errors}", result.StringMessage)
                    );
                }

                return result.Match
                (
                    onSuccess: () => Results.Ok(response),
                    onFailure: error => Results.BadRequest(error)
                );
            }).RequireAuthorization();
        }
    }
}