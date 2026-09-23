using MoneyFlow.Bff.Services;
using Microsoft.AspNetCore.Mvc;

namespace MoneyFlow.Bff.Features.User
{
    public static class UserEndpoints
    {
        public static void MapUser(this IEndpointRouteBuilder app)
        {
            app.MapGet("api/v1/check/admin", async (HttpContext context, [FromServices] IJwtReader jwtReader, CancellationToken ct = default) =>
            {
                var accessToken = context.Items["AccessToken"] as string;
                
                var jwtDTO = jwtReader.Extract(accessToken!);

                var adminRole = jwtDTO.Roles.FirstOrDefault(r => r == "Admin");

                if (string.IsNullOrWhiteSpace(adminRole))
                    return Results.StatusCode(StatusCodes.Status403Forbidden);

                return Results.Ok();
            }).RequireAuthorization();
        }
    }
}