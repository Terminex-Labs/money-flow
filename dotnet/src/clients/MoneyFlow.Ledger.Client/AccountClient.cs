using Shared.Http;
using System.Text.Json;
using System.Net.Http.Json;
using Terminex.Common.Results;
using Shared.Client.Abstraction;
using Terminex.Common.Primitives;
using Microsoft.Extensions.Options;
using Shared.Ledger.Contracts.Accounts.Request;
using Shared.Ledger.Contracts.Accounts.Response;

namespace MoneyFlow.Ledger.Client
{
    public sealed class AccountClient(HttpClient http, IOptions<JsonSerializerOptions> options) : HttpService(http, options.Value), IAccountClient
    {
        private readonly string _url = "api/v1/account";

        public async Task<Result<CreatedAccountResponse>> CreateAsync(CreateAccountRequest request, CancellationToken ctn = default)
            => await CatchResponseAsync<CreatedAccountResponse>(async ct => await _http.PostAsJsonAsync(_url, request, _jsonOptions, ct), ctn);

        public async Task<Result<List<AccountResponse>>> GetAllAsync(CancellationToken ctn = default)
            => await CatchResponseAsync<List<AccountResponse>>(async ct => await _http.GetAsync(_url, ct), ctn);

        public async Task<Result<Nothing>> UpdateNameAsync(UpdateAccountNameRequest request, CancellationToken ctn = default)
            => await CatchAsync(async ct => await _http.PatchAsJsonAsync($"{_url}/name", request, _jsonOptions, ct), ctn);

        public async Task<Result<Nothing>> FreezeAsync(Guid id, CancellationToken ctn = default)
            => await CatchAsync(async ct => await _http.PatchAsJsonAsync($"{_url}/freeze/{id}", _jsonOptions, ct), ctn);

        public async Task<Result<Nothing>> UnfreezeAsync(Guid id, CancellationToken ctn = default)
            => await CatchAsync(async ct => await _http.PatchAsJsonAsync($"{_url}/unfreeze/{id}", _jsonOptions, ct), ctn);

        public async Task<Result<Nothing>> DeleteAsync(Guid id, CancellationToken ctn = default)
            => await CatchAsync(async ct => await _http.DeleteAsync($"{_url}/{id}", ct), ctn);
    }
}