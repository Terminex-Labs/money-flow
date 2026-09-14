using Shared.Kernel.Abstractions;
using MoneyFlow.Catalog.Domain.Models;

namespace MoneyFlow.Catalog.Application.Abstractions.Repositories.Currencies
{
    public interface ICurrencyReadOnlyRepository : IReadOnlyRepository<Currency>;
}