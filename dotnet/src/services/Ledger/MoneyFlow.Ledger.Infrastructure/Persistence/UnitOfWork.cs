using MoneyFlow.Ledger.Application.Abstractions.UnitOfWork;
using MoneyFlow.Ledger.Infrastructure.Persistence.Contexts;

namespace MoneyFlow.Ledger.Infrastructure.Persistence
{
    internal class UnitOfWork(LedgerContext context) : IUnitOfWork
    {
        private readonly LedgerContext _context = context;

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