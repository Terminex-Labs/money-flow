using Shared.Dapper;
using System.Data.Common;
using MoneyFlow.Catalog.Domain.Models;
using MoneyFlow.Catalog.Infrastructure.Persistence.Constants;
using MoneyFlow.Catalog.Application.Abstractions.Repositories.Currencies;

namespace MoneyFlow.Catalog.Infrastructure.Persistence.Repositories.Currencies
{
    internal sealed class CurrencyReadOnlyRepository(DbConnection connection) : ReadOnlyRepository<Currency>(connection, TableNames.Currency), ICurrencyReadOnlyRepository
    {
        
    }
}