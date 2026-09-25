using Shared.Http;
using System.Text.Json;
using System.Net.Http.Json;
using Terminex.Common.Results;
using Shared.Client.Abstraction;
using Terminex.Common.Primitives;
using Microsoft.Extensions.Options;
using Shared.Catalog.Contracts.Request;
using Shared.Catalog.Contracts.Response;

namespace MoneyFlow.Catalog.Client
{
    public sealed class TypeAccountClient(HttpClient http, IOptions<JsonSerializerOptions> options) : HttpService(http, options.Value), ITypeAccountClient
    {
        private readonly string _url = "api/v1/type/account";

        public async Task<Result<CreatedTypeAccountResponse>> CreateAsync(CreateTypeAccountRequest request, CancellationToken ctn = default)
            => await CatchResponseAsync<CreatedTypeAccountResponse>(async ct => await _http.PostAsJsonAsync(_url, request, _jsonOptions, ct), ctn);

        public async Task<Result<List<TypeAccountResponse>>> GetAllAsync(CancellationToken ctn = default)
            => await CatchResponseAsync<List<TypeAccountResponse>>(async ct => await _http.GetAsync(_url, ct), ctn);

        public async Task<Result<TypeAccountResponse>> GetByIdAsync(Guid id, CancellationToken ctn = default)
            => await CatchResponseAsync<TypeAccountResponse>(async ct => await _http.GetAsync($"{_url}/{id}", ct), ctn);

        public async Task<Result<Nothing>> UpdateAsync(UpdateTypeAccountRequest request, CancellationToken ctn = default)
            => await CatchAsync(async ct => await _http.PatchAsJsonAsync(_url, request, _jsonOptions, ct), ctn);

        public async Task<Result<Nothing>> DeleteAsync(Guid id, CancellationToken ctn = default)
            => await CatchAsync(async ct => await _http.DeleteAsync($"{_url}/{id}", ct), ctn);
    }
}