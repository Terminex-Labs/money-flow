using Shared.Http;
using System.Text.Json;
using Terminex.Common.Results;
using Shared.Client.Abstraction;
using Microsoft.Extensions.Options;
using Shared.IdentityHub.Contracts.Response;

namespace MoneyFlow.IdentityHub.Client
{
    public sealed class IdentityHubClient(HttpClient httpClient, IOptions<JsonSerializerOptions> options) : HttpService(httpClient, options.Value), IIdentityHubClient
    {
        public async Task<Result<UserInfoResponse>> GetUserInfoAsync(string userId, CancellationToken ctn = default)
            => await CatchResponseAsync<UserInfoResponse>(async ct => await _http.GetAsync($"api/users/general-info/{userId}", ct), ctn);
    }
}