using MediatR;
using Terminex.Common.Results;
using Terminex.Common.Primitives;
using MoneyFlow.Catalog.Application.Abstractions.UnitOfWork;
using MoneyFlow.Catalog.Application.Abstractions.Repositories.TypeAccounts;

namespace MoneyFlow.Catalog.Application.Features.TypeAccounts.Commands.Delete
{
    public sealed class DeleteTypeAccountHandler(ITypeAccountRepository repository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteTypeAccountCommand, Result<Nothing>>
    {
        public async Task<Result<Nothing>> Handle(DeleteTypeAccountCommand request, CancellationToken cancellationToken)
        {
            var maybeTypeAccount = await repository.GetByAsync(typeAccount => typeAccount.Id == request.Id);

            if (maybeTypeAccount.IsNone)
                return Error.NotFound("Тип счета не найден!");

            repository.Remove(maybeTypeAccount.Value);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Nothing.Value;
        }
    }
}