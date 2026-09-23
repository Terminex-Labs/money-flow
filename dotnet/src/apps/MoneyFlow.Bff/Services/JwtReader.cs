using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MoneyFlow.Bff.Services
{
    public sealed class JwtReader : IJwtReader
    {
        private static readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new();

        public JwtReaderDTO Extract(string token)
        {
            var result = _jwtSecurityTokenHandler.ReadJwtToken(token);

            var userId = result.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)!.Value;
            var login = result.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Name)!.Value;
            var roles = result.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).ToList();
            var expire = DateTimeOffset.FromUnixTimeSeconds(long.Parse(result.Claims.First(x => x.Type == "exp").Value)).UtcDateTime;

            return new JwtReaderDTO(userId, login, expire, roles);
        }
    }

    public sealed record JwtReaderDTO(string UserId, string Login, DateTime ExpiredTime, List<string> Roles);
}