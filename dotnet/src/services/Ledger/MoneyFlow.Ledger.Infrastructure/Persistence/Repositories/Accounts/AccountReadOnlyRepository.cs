using Dapper;
using System.Data;
using Shared.Dapper;
using Shared.Ledger.Contracts.Accounts.Response;
using MoneyFlow.Ledger.Infrastructure.Persistence.Constants;
using MoneyFlow.Ledger.Application.Abstractions.Repositories.Accounts;

namespace MoneyFlow.Ledger.Infrastructure.Persistence.Repositories.Accounts
{
    internal sealed class AccountReadOnlyRepository(IDbConnection connection) : ReadOnlyRepository<AccountResponse>(connection, TableNames.Account), IAccountReadOnlyRepository
    {
        public async Task<List<AccountResponse>> GetAllAsync(Guid userId, CancellationToken ct)
        {
            string query = SqlLoader.Load<AccountReadOnlyRepository>("GetAllAccountByUserId.sql");

            var accounts = await _connection.QueryAsync<AccountResponse>(query, new { userId });

            return [.. accounts];
        }

        public async Task<AccountResponse> GetByIdAsync(Guid userId, Guid accountId, CancellationToken ct)
        {
            string query = SqlLoader.Load<AccountReadOnlyRepository>("GetByIdAccountByUserId.sql");

            var account = await _connection.QueryFirstAsync<AccountResponse>(query, new { userId, accountId });

            return account;
        }
    }
}