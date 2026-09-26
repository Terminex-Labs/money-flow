using MediatR;
using Terminex.Common.Results;
using Shared.Catalog.Contracts.Response;
using MoneyFlow.Catalog.Application.Abstractions.Repositories.TypeTransactions;

namespace MoneyFlow.Catalog.Application.Features.TypeTransactions.Queries.ById
{
    public sealed class GetByIdTypeTransactionHandler(ITypeTransactionReadOnlyRepository repository) 
        : IRequestHandler<GetByIdTypeTransactionQuery, Result<TypeTransactionResponse>>
    {
        public async Task<Result<TypeTransactionResponse>> Handle(GetByIdTypeTransactionQuery request, CancellationToken cancellationToken)
        {
            var typeTransaction = await repository.GetByIdAsync(request.Id, cancellationToken);

            if (typeTransaction == null)
                return Error.NotFound("Тип транзакции не найден!");

            return typeTransaction;
        }
    }
}