using MediatR;
using Terminex.Common.Results;
using Shared.Catalog.Contracts.Response;
using MoneyFlow.Catalog.Application.Abstractions.Repositories.TypeTransactions;

namespace MoneyFlow.Catalog.Application.Features.TypeTransactions.Queries.All
{
    public sealed class GetAllTypeTransactionHandler(ITypeTransactionReadOnlyRepository repository) 
        : IRequestHandler<GetAllTypeTransactionQuery, Result<List<TypeTransactionResponse>>>
    {
        public async Task<Result<List<TypeTransactionResponse>>> Handle(GetAllTypeTransactionQuery request, CancellationToken cancellationToken)
        {
            var typeTransactions = await repository.GetAllAsync(cancellationToken);

            return typeTransactions;
        }
    }
}