using Terminex.Common.Results;
using Shared.IdentityHub.Contracts.Response;

namespace Shared.Client.Abstraction
{
    public interface IIdentityHubClient
    {
        Task<Result<UserInfoResponse>> GetUserInfoAsync(string userId, CancellationToken ctn = default);
    }
}