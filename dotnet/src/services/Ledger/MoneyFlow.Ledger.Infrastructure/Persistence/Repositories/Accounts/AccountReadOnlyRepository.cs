using Shared.Dapper;
using System.Data.Common;
using MoneyFlow.Ledger.Domain.Models;
using MoneyFlow.Ledger.Infrastructure.Persistence.Constants;
using MoneyFlow.Ledger.Application.Abstractions.Repositories.Accounts;

namespace MoneyFlow.Ledger.Infrastructure.Persistence.Repositories.Accounts
{
    internal sealed class AccountReadOnlyRepository(DbConnection connection) : ReadOnlyRepository<Account>(connection, TableNames.Account), IAccountReadOnlyRepository
    {
        
    }
}