using Terminex.Common.Results;
using Shared.IdentityHub.Contracts.Response;

namespace MoneyFlow.IdentityHub.Client
{
    public interface IIdentityHubClient
    {
        Task<Result<UserInfoResponse>> GetUserInfoAsync(string userId, CancellationToken ctn = default);
    }
}