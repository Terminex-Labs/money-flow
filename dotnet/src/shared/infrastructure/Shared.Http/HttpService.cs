using System.Text.Json;
using Terminex.Common.Results;
using Terminex.Common.Primitives;

namespace Shared.Http
{
    public abstract class HttpService(HttpClient http, JsonSerializerOptions? jsonOptions = null)
    {
        protected readonly HttpClient _http = http;
        protected readonly JsonSerializerOptions _jsonOptions = jsonOptions ?? new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        protected async Task<Result<TResponse>> CatchResponseAsync<TResponse>(Func<CancellationToken, Task<HttpResponseMessage>> func, CancellationToken ct = default) 
            => await HttpResult.CatchResponseAsync<TResponse>(func, _jsonOptions, ct);

        protected async Task<Result<Nothing>> CatchAsync(Func<CancellationToken, Task<HttpResponseMessage>> func, CancellationToken ct = default)
            => await HttpResult.CatchAsync(func, ct);
    }
}