using Shared.EntityFramework;
using MoneyFlow.Catalog.Domain.Models;
using MoneyFlow.Catalog.Infrastructure.Persistence.Contexts;
using MoneyFlow.Catalog.Application.Abstractions.Repositories.Currencies;

namespace MoneyFlow.Catalog.Infrastructure.Persistence.Repositories.Currencies
{
    internal sealed class CurrencyRepository(CatalogContext writeContext) : Repository<Currency, CatalogContext>(writeContext), ICurrencyRepository
    {
        
    }
}