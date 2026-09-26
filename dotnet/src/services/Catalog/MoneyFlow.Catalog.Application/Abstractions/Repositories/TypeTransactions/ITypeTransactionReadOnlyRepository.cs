using Shared.Catalog.Contracts.Response;

namespace MoneyFlow.Catalog.Application.Abstractions.Repositories.TypeTransactions
{
    public interface ITypeTransactionReadOnlyRepository
    {
        Task<List<TypeTransactionResponse>> GetAllAsync(CancellationToken ct = default);
        Task<TypeTransactionResponse> GetByIdAsync(Guid typeTransactionId, CancellationToken ct = default);
    }
}