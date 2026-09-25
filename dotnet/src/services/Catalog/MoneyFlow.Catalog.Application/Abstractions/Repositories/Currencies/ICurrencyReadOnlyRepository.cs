using Shared.Kernel.Abstractions;
using Shared.Catalog.Contracts.Response;

namespace MoneyFlow.Catalog.Application.Abstractions.Repositories.Currencies
{
    public interface ICurrencyReadOnlyRepository : IReadOnlyRepository<CurrencyResponse>
    {
        new Task<List<CurrencyResponse>> GetAllAsync(CancellationToken ct = default);
        Task<CurrencyResponse> GetByIdAsync(Guid currencyId, CancellationToken ct = default);
    }
}