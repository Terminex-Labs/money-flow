using Dapper;
using System.Data;
using Shared.Dapper;
using Shared.Catalog.Contracts.Response;
using MoneyFlow.Catalog.Infrastructure.Persistence.Constants;
using MoneyFlow.Catalog.Application.Abstractions.Repositories.TypeTransactions;

namespace MoneyFlow.Catalog.Infrastructure.Persistence.Repositories.TypeTransactions
{
    internal sealed class TypeTransactionReadOnlyRepository(IDbConnection connection) 
        : ReadOnlyRepository<TypeTransactionResponse>(connection, TableNames.TypeTransaction), ITypeTransactionReadOnlyRepository
    {
        public async new Task<List<TypeTransactionResponse>> GetAllAsync(CancellationToken ct = default)
        {
            string query = SqlLoader.Load<TypeTransactionReadOnlyRepository>("GetAllTypeTransaction.sql");

            var typeTransactions = await _connection.QueryAsync<TypeTransactionResponse>(query);

            return [.. typeTransactions];
        }

        public async Task<TypeTransactionResponse> GetByIdAsync(Guid typeTransactionId, CancellationToken ct = default)
        {
            string query = SqlLoader.Load<TypeTransactionReadOnlyRepository>("GetByIdTypeTransaction.sql");

            var typeTransaction = await _connection.QueryFirstAsync<TypeTransactionResponse>(query, new { typeTransactionId });

            return typeTransaction;
        }
    }
}