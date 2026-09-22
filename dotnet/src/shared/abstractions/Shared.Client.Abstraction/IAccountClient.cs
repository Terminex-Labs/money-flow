using Terminex.Common.Results;
using Terminex.Common.Primitives;
using Shared.Ledger.Contracts.Accounts.Request;
using Shared.Ledger.Contracts.Accounts.Response;

namespace Shared.Client.Abstraction
{
    public interface IAccountClient
    {
        Task<Result<CreatedAccountResponse>> CreateAsync(CreateAccountRequest request, CancellationToken ctn = default);
        Task<Result<List<AccountResponse>>> GetAllAsync(CancellationToken ctn = default);
        Task<Result<AccountResponse>> GetByIdAsync(Guid id, CancellationToken ctn = default);
        Task<Result<Nothing>> UpdateAsync(UpdateAccountRequest request, CancellationToken ctn = default);
        Task<Result<Nothing>> FreezeAsync(Guid id, CancellationToken ctn = default);
        Task<Result<Nothing>> UnfreezeAsync(Guid id, CancellationToken ctn = default);
        Task<Result<Nothing>> DeleteAsync(Guid id, CancellationToken ctn = default);
    }
}