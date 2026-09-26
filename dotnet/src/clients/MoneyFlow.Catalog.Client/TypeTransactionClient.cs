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
    public sealed class TypeTransactionClient(HttpClient http, IOptions<JsonSerializerOptions> options) : HttpService(http, options.Value), ITypeTransactionClient
    {
        private readonly string _url = "api/v1/type/transaction";

        public async Task<Result<CreatedTypeTransactionResponse>> CreateAsync(CreateTypeTransactionRequest request, CancellationToken ctn = default)
            => await CatchResponseAsync<CreatedTypeTransactionResponse>(async ct => await _http.PostAsJsonAsync(_url, request, _jsonOptions, ct), ctn);

        public async Task<Result<List<TypeTransactionResponse>>> GetAllAsync(CancellationToken ctn = default)
            => await CatchResponseAsync<List<TypeTransactionResponse>>(async ct => await _http.GetAsync(_url, ct), ctn);

        public async Task<Result<TypeTransactionResponse>> GetByIdAsync(Guid id, CancellationToken ctn = default)
            => await CatchResponseAsync<TypeTransactionResponse>(async ct => await _http.GetAsync($"{_url}/{id}", ct), ctn);

        public async Task<Result<Nothing>> UpdateAsync(UpdateTypeTransactionRequest request, CancellationToken ctn = default)
            => await CatchAsync(async ct => await _http.PatchAsJsonAsync(_url, request, _jsonOptions, ct), ctn);

        public async Task<Result<Nothing>> DeleteAsync(Guid id, CancellationToken ctn = default)
            => await CatchAsync(async ct => await _http.DeleteAsync($"{_url}/{id}", ct), ctn);
    }
}