using Terminex.Common.Results;
using Terminex.Common.Primitives;
using Shared.Catalog.Contracts.Request;
using Shared.Catalog.Contracts.Response;

namespace Shared.Client.Abstraction
{
    public interface ICurrencyClient
    {
        Task<Result<CreatedCurrencyResponse>> CreateAsync(CreateCurrencyRequest request, CancellationToken ctn = default);
        Task<Result<List<CurrencyResponse>>> GetAllAsync(CancellationToken ctn = default);
        Task<Result<Nothing>> UpdateAsync(UpdateCurrencyRequest request, CancellationToken ctn = default);
        Task<Result<Nothing>> DeleteAsync(Guid id, CancellationToken ctn = default);
    }
}