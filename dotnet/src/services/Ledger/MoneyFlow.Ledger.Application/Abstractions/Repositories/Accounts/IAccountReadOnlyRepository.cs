using Shared.Kernel.Abstractions;
using Shared.Ledger.Contracts.Accounts.Response;

namespace MoneyFlow.Ledger.Application.Abstractions.Repositories.Accounts
{
    public interface IAccountReadOnlyRepository : IReadOnlyRepository<AccountResponse>
    {
        
    }
}