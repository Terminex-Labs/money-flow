using System.Data;
using Shared.Dapper;
using Shared.Catalog.Contracts.Response;
using MoneyFlow.Catalog.Infrastructure.Persistence.Constants;
using MoneyFlow.Catalog.Application.Abstractions.Repositories.TypeAccounts;

namespace MoneyFlow.Catalog.Infrastructure.Persistence.Repositories.TypeAccounts
{
    internal sealed class TypeAccountReadOnlyRepository(IDbConnection connection) : ReadOnlyRepository<TypeAccountResponse>(connection, TableNames.TypeAccount), ITypeAccountReadOnlyRepository
    {
        
    }
}