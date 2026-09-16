using System.Data;
using Shared.Dapper;
using Shared.Catalog.Contracts.Response;
using MoneyFlow.Catalog.Infrastructure.Persistence.Constants;
using MoneyFlow.Catalog.Application.Abstractions.Repositories.Currencies;

namespace MoneyFlow.Catalog.Infrastructure.Persistence.Repositories.Currencies
{
    internal sealed class CurrencyReadOnlyRepository(IDbConnection connection) : ReadOnlyRepository<CurrencyResponse>(connection, TableNames.Currency), ICurrencyReadOnlyRepository
    {
        
    }
}