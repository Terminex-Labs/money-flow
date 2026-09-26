using MediatR;
using Terminex.Common.Results;
using MoneyFlow.Catalog.Domain.Models;
using Shared.Catalog.Contracts.Response;
using MoneyFlow.Catalog.Domain.ValueObjects.TypesAccounts;
using MoneyFlow.Catalog.Application.Abstractions.UnitOfWork;
using MoneyFlow.Catalog.Application.Abstractions.Repositories.TypeTransactions;

namespace MoneyFlow.Catalog.Application.Features.TypeTransactions.Commands.Create
{
    public sealed class CreateTypeTransactionHandler(ITypeTransactionRepository repository, IUnitOfWork unitOfWork) 
        : IRequestHandler<CreateTypeTransactionCommand, Result<CreatedTypeTransactionResponse>>
    {
        public async Task<Result<CreatedTypeTransactionResponse>> Handle(CreateTypeTransactionCommand request, CancellationToken cancellationToken)
        {
            var typeTransaction = TypeTransaction.Create(TypeTransactionName.Create(request.Name));

            await repository.AddAsync(typeTransaction, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return new CreatedTypeTransactionResponse(typeTransaction.Id);
        }
    }
}