using MediatR;
using Terminex.Common.Results;
using Terminex.Common.Primitives;
using MoneyFlow.Catalog.Domain.ValueObjects.TypesAccounts;
using MoneyFlow.Catalog.Application.Abstractions.UnitOfWork;
using MoneyFlow.Catalog.Application.Abstractions.Repositories.TypeAccounts;

namespace MoneyFlow.Catalog.Application.Features.TypeAccounts.Commands.Update
{
    public sealed class UpdateTypeAccountHandler(ITypeAccountRepository repository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateTypeAccountCommand, Result<Nothing>>
    {
        public async Task<Result<Nothing>> Handle(UpdateTypeAccountCommand request, CancellationToken cancellationToken)
        {
            var maybeTypeAccount = await repository.GetByAsync(typeAccount => typeAccount.Id == request.Id);

            if (maybeTypeAccount.IsNone)
                return Error.NotFound("Тип счета не найден!");

            maybeTypeAccount.Value.UpdateName(TypeAccountName.Create(request.Name));
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Nothing.Value;
        }
    }
}