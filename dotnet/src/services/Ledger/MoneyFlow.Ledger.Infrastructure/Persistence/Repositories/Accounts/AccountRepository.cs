using Shared.EntityFramework;
using MoneyFlow.Ledger.Domain.Models;
using MoneyFlow.Ledger.Infrastructure.Persistence.Contexts;
using MoneyFlow.Ledger.Application.Abstractions.Repositories.Accounts;

namespace MoneyFlow.Ledger.Infrastructure.Persistence.Repositories.Accounts
{
    internal sealed class AccountRepository(LedgerContext context) : Repository<Account, LedgerContext>(context), IAccountRepository
    {
        
    }
}