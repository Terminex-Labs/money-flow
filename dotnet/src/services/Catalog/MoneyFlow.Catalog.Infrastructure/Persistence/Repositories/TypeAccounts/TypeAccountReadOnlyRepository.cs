using Dapper;
using System.Data;
using Shared.Dapper;
using Shared.Catalog.Contracts.Response;
using MoneyFlow.Catalog.Infrastructure.Persistence.Constants;
using MoneyFlow.Catalog.Application.Abstractions.Repositories.TypeAccounts;

namespace MoneyFlow.Catalog.Infrastructure.Persistence.Repositories.TypeAccounts
{
    internal sealed class TypeAccountReadOnlyRepository(IDbConnection connection) : ReadOnlyRepository<TypeAccountResponse>(connection, TableNames.TypeAccount), ITypeAccountReadOnlyRepository
    {
        public async new Task<List<TypeAccountResponse>> GetAllAsync(CancellationToken ct = default)
        {
            string query = SqlLoader.Load<TypeAccountReadOnlyRepository>("GetAllTypeAccount.sql");

            var typeAccounts = await _connection.QueryAsync<TypeAccountResponse>(query);

            return [.. typeAccounts];
        }

        public async Task<TypeAccountResponse> GetByIdAsync(Guid typeAccountId, CancellationToken ct = default)
        {
            string query = SqlLoader.Load<TypeAccountReadOnlyRepository>("GetByIdTypeAccount.sql");

            var typeAccount = await _connection.QueryFirstAsync<TypeAccountResponse>(query, new { typeAccountId });

            return typeAccount;
        } 
    }
}