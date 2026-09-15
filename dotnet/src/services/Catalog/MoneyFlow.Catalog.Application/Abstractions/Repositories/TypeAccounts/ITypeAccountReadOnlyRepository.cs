using Shared.Kernel.Abstractions;
using Shared.Catalog.Contracts.Response;

namespace MoneyFlow.Catalog.Application.Abstractions.Repositories.TypeAccounts
{
    public interface ITypeAccountReadOnlyRepository : IReadOnlyRepository<TypeAccountResponse>
    {
        
    }
}