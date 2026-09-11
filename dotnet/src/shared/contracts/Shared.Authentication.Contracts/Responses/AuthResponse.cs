namespace Shared.Authentication.Contracts.Responses
{
    public sealed record AuthResponse(string AccessToken, string RefreshToken, string? M2 = null);
}