using Terminex.Common.Results;
using Shared.Authentication.Contracts.Requests;
using Shared.Authentication.Contracts.Responses;

namespace Shared.Client.Abstraction
{
    public interface IAuthClient
    {
        Task<Result<AuthResponse>> RefreshToken(RefreshTokenRequest request, CancellationToken ctn = default);
    }
}