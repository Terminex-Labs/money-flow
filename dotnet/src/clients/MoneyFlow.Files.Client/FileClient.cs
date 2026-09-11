using Shared.Http;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Terminex.Common.Results;
using Shared.Files.Contracts.Response;
using Shared.Files.Contracts.Request;
using System.Net.Http.Json;

namespace MoneyFlow.Files.Client
{
    public sealed class FileClient(HttpClient httpClient, IOptions<JsonSerializerOptions> options) : HttpService(httpClient, options.Value), IFileClient
    {
        public async Task<Result<PresignedUrlResponse>> GetPresignedUrlAsync(PresignedUrlRequest request, CancellationToken ctn = default)
            => await CatchResponseAsync<PresignedUrlResponse>(async ct => await _http.PostAsJsonAsync("presigned-url", request, ct), ctn);
    }
}