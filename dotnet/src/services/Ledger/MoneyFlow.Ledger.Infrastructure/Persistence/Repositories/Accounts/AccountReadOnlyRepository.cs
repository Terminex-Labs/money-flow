using Shared.Dapper;
using System.Data.Common;
using Shared.Ledger.Contracts.Accounts.Response;
using MoneyFlow.Ledger.Infrastructure.Persistence.Constants;
using MoneyFlow.Ledger.Application.Abstractions.Repositories.Accounts;

namespace MoneyFlow.Ledger.Infrastructure.Persistence.Repositories.Accounts
{
    internal sealed class AccountReadOnlyRepository(DbConnection connection) : ReadOnlyRepository<AccountResponse>(connection, TableNames.Account), IAccountReadOnlyRepository
    {
        
    }
}