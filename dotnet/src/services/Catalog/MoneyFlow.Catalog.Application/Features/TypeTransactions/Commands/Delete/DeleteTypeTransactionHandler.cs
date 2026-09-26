using MediatR;
using Terminex.Common.Results;
using Terminex.Common.Primitives;
using MoneyFlow.Catalog.Application.Abstractions.UnitOfWork;
using MoneyFlow.Catalog.Application.Abstractions.Repositories.TypeTransactions;

namespace MoneyFlow.Catalog.Application.Features.TypeTransactions.Commands.Delete
{
    public sealed class DeleteTypeTransactionHandler(ITypeTransactionRepository repository, IUnitOfWork unitOfWork) 
        : IRequestHandler<DeleteTypeTransactionCommand, Result<Nothing>>
    {
        public async Task<Result<Nothing>> Handle(DeleteTypeTransactionCommand request, CancellationToken cancellationToken)
        {
            var maybeTypeTransaction = await repository.GetByAsync(tt => tt.Id == request.Id, cancellationToken);

            if (maybeTypeTransaction.IsNone)
                return Error.NotFound("Тип транзакции не найден!");

            repository.Remove(maybeTypeTransaction.Value);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Nothing.Value;
        }
    }
}