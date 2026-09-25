using Terminex.Common.Results;
using Terminex.Common.Primitives;
using Shared.Catalog.Contracts.Request;
using Shared.Catalog.Contracts.Response;

namespace Shared.Client.Abstraction
{
    public interface ITypeAccountClient
    {
        Task<Result<CreatedTypeAccountResponse>> CreateAsync(CreateTypeAccountRequest request, CancellationToken ctn = default);
        Task<Result<List<TypeAccountResponse>>> GetAllAsync(CancellationToken ctn = default);
        Task<Result<TypeAccountResponse>> GetByIdAsync(Guid id, CancellationToken ctn = default);
        Task<Result<Nothing>> UpdateAsync(UpdateTypeAccountRequest request, CancellationToken ctn = default);
        Task<Result<Nothing>> DeleteAsync(Guid id, CancellationToken ctn = default);
    }
}