using Shared.Dapper;
using System.Data.Common;
using MoneyFlow.Catalog.Domain.Models;
using MoneyFlow.Catalog.Infrastructure.Persistence.Constants;
using MoneyFlow.Catalog.Application.Abstractions.Repositories.TypeAccounts;

namespace MoneyFlow.Catalog.Infrastructure.Persistence.Repositories.TypeAccounts
{
    internal sealed class TypeAccountReadOnlyRepository(DbConnection connection) : ReadOnlyRepository<TypeAccount>(connection, TableNames.TypeAccount), ITypeAccountReadOnlyRepository
    {
        
    }
}