using Shared.EntityFramework;
using MoneyFlow.Catalog.Domain.Models;
using MoneyFlow.Catalog.Infrastructure.Persistence.Contexts;
using MoneyFlow.Catalog.Application.Abstractions.Repositories.TypeAccounts;

namespace MoneyFlow.Catalog.Infrastructure.Persistence.Repositories.TypeAccounts
{
    internal sealed class TypeAccountRepository(CatalogContext context) : Repository<TypeAccount, CatalogContext>(context), ITypeAccountRepository
    {
        
    }
}