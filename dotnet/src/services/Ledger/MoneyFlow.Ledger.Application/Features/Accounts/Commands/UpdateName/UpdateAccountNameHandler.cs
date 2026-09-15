using MediatR;
using Terminex.Common.Results;
using Terminex.Common.Primitives;
using MoneyFlow.Ledger.Domain.ValueObjects.Accounts;
using MoneyFlow.Ledger.Application.Abstractions.UnitOfWork;
using MoneyFlow.Ledger.Application.Abstractions.Repositories.Accounts;

namespace MoneyFlow.Ledger.Application.Features.Accounts.Commands.UpdateName
{
    public sealed class UpdateAccountNameHandler(IAccountRepository repository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateAccountNameCommand, Result<Nothing>>
    {
        public async Task<Result<Nothing>> Handle(UpdateAccountNameCommand request, CancellationToken cancellationToken)
        {
            var maybeAccount = await repository.GetByAsync(account => account.Id == request.Id);

            if (maybeAccount.IsNone)
                return Error.NotFound("Счет не найден!");

            maybeAccount.Value.UpdateName(AccountName.Create(request.Name));
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Nothing.Value;
        }
    }
}