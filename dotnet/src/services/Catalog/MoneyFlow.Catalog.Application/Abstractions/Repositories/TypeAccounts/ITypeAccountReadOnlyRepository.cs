using Shared.Kernel.Abstractions;
using Shared.Catalog.Contracts.Response;

namespace MoneyFlow.Catalog.Application.Abstractions.Repositories.TypeAccounts
{
    public interface ITypeAccountReadOnlyRepository : IReadOnlyRepository<TypeAccountResponse>
    {
        new Task<List<TypeAccountResponse>> GetAllAsync(CancellationToken ct = default);
        Task<TypeAccountResponse> GetByIdAsync(Guid typeAccountId, CancellationToken ct = default);
    }
}