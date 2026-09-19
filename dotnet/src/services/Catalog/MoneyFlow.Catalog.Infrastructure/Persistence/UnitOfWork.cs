using MoneyFlow.Catalog.Application.Abstractions.UnitOfWork;
using MoneyFlow.Catalog.Infrastructure.Persistence.Contexts;

namespace MoneyFlow.Catalog.Infrastructure.Persistence
{
    internal class UnitOfWork(CatalogContext context) : IUnitOfWork
    {
        private readonly CatalogContext _context = context;

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}