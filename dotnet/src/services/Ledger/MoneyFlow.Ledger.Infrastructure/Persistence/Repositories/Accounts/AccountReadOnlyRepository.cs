using System.Data;
using Shared.Dapper;
using Shared.Ledger.Contracts.Accounts.Response;
using MoneyFlow.Ledger.Infrastructure.Persistence.Constants;
using MoneyFlow.Ledger.Application.Abstractions.Repositories.Accounts;

namespace MoneyFlow.Ledger.Infrastructure.Persistence.Repositories.Accounts
{
    internal sealed class AccountReadOnlyRepository(IDbConnection connection) : ReadOnlyRepository<AccountResponse>(connection, TableNames.Account), IAccountReadOnlyRepository
    {
        
    }
}