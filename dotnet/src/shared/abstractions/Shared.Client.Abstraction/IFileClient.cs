using Terminex.Common.Results;
using Shared.Files.Contracts.Request;
using Shared.Files.Contracts.Response;

namespace Shared.Client.Abstraction
{
    public interface IFileClient
    {
        Task<Result<PresignedUrlResponse>> GetPresignedUrlAsync(PresignedUrlRequest request, CancellationToken ctn = default);
    }
}