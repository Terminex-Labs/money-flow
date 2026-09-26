using MediatR;
using Terminex.Common.Results;
using Terminex.Common.Primitives;
using MoneyFlow.Catalog.Domain.ValueObjects.TypesAccounts;
using MoneyFlow.Catalog.Application.Abstractions.UnitOfWork;
using MoneyFlow.Catalog.Application.Abstractions.Repositories.TypeTransactions;

namespace MoneyFlow.Catalog.Application.Features.TypeTransactions.Commands.Update
{
    public sealed class UpdateTypeTransactionHandler(ITypeTransactionRepository repository, IUnitOfWork unitOfWork)
        : IRequestHandler<UpdateTypeTransactionCommand, Result<Nothing>>
    {
        public async Task<Result<Nothing>> Handle(UpdateTypeTransactionCommand request, CancellationToken cancellationToken)
        {
            var maybeTypeTransaction = await repository.GetByAsync(tt => tt.Id == request.Id, cancellationToken);

            if (maybeTypeTransaction.IsNone)
                return Error.NotFound("Тип транзакции не найден!");

            maybeTypeTransaction.Value.UpdateName(TypeTransactionName.Create(request.Name));
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Nothing.Value;
        }
    }
}