using Shared.Kernel.Abstractions;
using Shared.Ledger.Contracts.Accounts.Response;

namespace MoneyFlow.Ledger.Application.Abstractions.Repositories.Accounts
{
    public interface IAccountReadOnlyRepository : IReadOnlyRepository<AccountResponse>
    {
        Task<List<AccountResponse>> GetAllAsync(Guid userId, CancellationToken ct);
        Task<AccountResponse> GetByIdAsync(Guid userId, Guid accountId, CancellationToken ct);
    }
}