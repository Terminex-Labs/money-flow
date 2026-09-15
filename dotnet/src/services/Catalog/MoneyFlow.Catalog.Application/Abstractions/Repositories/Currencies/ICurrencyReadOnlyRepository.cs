using Shared.Kernel.Abstractions;
using Shared.Catalog.Contracts.Response;

namespace MoneyFlow.Catalog.Application.Abstractions.Repositories.Currencies
{
    public interface ICurrencyReadOnlyRepository : IReadOnlyRepository<CurrencyResponse>;
}