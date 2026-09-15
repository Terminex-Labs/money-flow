using Shared.Dapper;
using System.Data.Common;
using Shared.Catalog.Contracts.Response;
using MoneyFlow.Catalog.Infrastructure.Persistence.Constants;
using MoneyFlow.Catalog.Application.Abstractions.Repositories.TypeAccounts;

namespace MoneyFlow.Catalog.Infrastructure.Persistence.Repositories.TypeAccounts
{
    internal sealed class TypeAccountReadOnlyRepository(DbConnection connection) : ReadOnlyRepository<TypeAccountResponse>(connection, TableNames.TypeAccount), ITypeAccountReadOnlyRepository
    {
        
    }
}