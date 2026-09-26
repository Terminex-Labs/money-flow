using Shared.EntityFramework;
using MoneyFlow.Catalog.Domain.Models;
using MoneyFlow.Catalog.Infrastructure.Persistence.Contexts;
using MoneyFlow.Catalog.Application.Abstractions.Repositories.TypeTransactions;

namespace MoneyFlow.Catalog.Infrastructure.Persistence.Repositories.TypeTransactions
{
    internal sealed class TypeTransactionRepository(CatalogContext context) : Repository<TypeTransaction, CatalogContext>(context), ITypeTransactionRepository
    {
        
    }
}