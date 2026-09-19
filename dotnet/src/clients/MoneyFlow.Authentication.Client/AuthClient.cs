using Shared.Http;
using System.Text.Json;
using System.Net.Http.Json;
using Terminex.Common.Results;
using Shared.Client.Abstraction;
using Microsoft.Extensions.Options;
using Shared.Authentication.Contracts.Requests;
using Shared.Authentication.Contracts.Responses;

namespace MoneyFlow.Authentication.Client
{
    public class AuthClient(HttpClient httpClient, IOptions<JsonSerializerOptions> options) : HttpService(httpClient, options.Value), IAuthClient
    {
        public async Task<Result<AuthResponse>> RefreshToken(RefreshTokenRequest request, CancellationToken ctn = default)
            => await CatchResponseAsync<AuthResponse>(async ct => await _http.PostAsJsonAsync("api/auth/refresh-token", request, _jsonOptions, ct), ctn);
    }
}