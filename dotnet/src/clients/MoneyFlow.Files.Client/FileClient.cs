using Shared.Http;
using System.Text.Json;
using System.Net.Http.Json;
using Terminex.Common.Results;
using Shared.Client.Abstraction;
using Microsoft.Extensions.Options;
using Shared.Files.Contracts.Request;
using Shared.Files.Contracts.Response;

namespace MoneyFlow.Files.Client
{
    public sealed class FileClient(HttpClient httpClient, IOptions<JsonSerializerOptions> options) : HttpService(httpClient, options.Value), IFileClient
    {
        public async Task<Result<PresignedUrlResponse>> GetPresignedUrlAsync(PresignedUrlRequest request, CancellationToken ctn = default)
            => await CatchResponseAsync<PresignedUrlResponse>(async ct => await _http.PostAsJsonAsync("presigned-url", request, ct), ctn);
    }
}