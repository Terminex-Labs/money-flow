using Shared.Kernel.Abstractions;
using MoneyFlow.Ledger.Domain.Models;

namespace MoneyFlow.Ledger.Application.Abstractions.Repositories.Accounts
{
    public interface IAccountReadOnlyRepository : IReadOnlyRepository<Account>
    {
        
    }
}