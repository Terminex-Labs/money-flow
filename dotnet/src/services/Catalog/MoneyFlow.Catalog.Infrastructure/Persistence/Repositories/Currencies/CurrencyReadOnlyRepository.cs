using Dapper;
using System.Data;
using Shared.Dapper;
using Shared.Catalog.Contracts.Response;
using MoneyFlow.Catalog.Infrastructure.Persistence.Constants;
using MoneyFlow.Catalog.Application.Abstractions.Repositories.Currencies;

namespace MoneyFlow.Catalog.Infrastructure.Persistence.Repositories.Currencies
{
    internal sealed class CurrencyReadOnlyRepository(IDbConnection connection) : ReadOnlyRepository<CurrencyResponse>(connection, TableNames.Currency), ICurrencyReadOnlyRepository
    {
        public async Task<CurrencyResponse> GetByIdAsync(Guid currencyId, CancellationToken ct = default)
        {
            string query = SqlLoader.Load<CurrencyReadOnlyRepository>("GetByIdCurrency.sql");

            var currency = await _connection.QueryFirstAsync<CurrencyResponse>(query, new { currencyId });

            return currency;
        }
    }
}