using Terminex.Common.Results;
using Terminex.Common.Primitives;
using Shared.Catalog.Contracts.Request;
using Shared.Catalog.Contracts.Response;

namespace Shared.Client.Abstraction
{
    public interface ITypeTransactionClient
    {
        Task<Result<CreatedTypeTransactionResponse>> CreateAsync(CreateTypeTransactionRequest request, CancellationToken ctn = default);
        Task<Result<List<TypeTransactionResponse>>> GetAllAsync(CancellationToken ctn = default);
        Task<Result<TypeTransactionResponse>> GetByIdAsync(Guid id, CancellationToken ctn = default);
        Task<Result<Nothing>> UpdateAsync(UpdateTypeTransactionRequest request, CancellationToken ctn = default);
        Task<Result<Nothing>> DeleteAsync(Guid id, CancellationToken ctn = default);
    }
}