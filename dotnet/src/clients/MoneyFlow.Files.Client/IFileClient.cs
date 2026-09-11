using Terminex.Common.Results;
using Shared.Files.Contracts.Request;
using Shared.Files.Contracts.Response;

namespace MoneyFlow.Files.Client
{
    public interface IFileClient
    {
        Task<Result<PresignedUrlResponse>> GetPresignedUrlAsync(PresignedUrlRequest request, CancellationToken ctn = default);
    }
}