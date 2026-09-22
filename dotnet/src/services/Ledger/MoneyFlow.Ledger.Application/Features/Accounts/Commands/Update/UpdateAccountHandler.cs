using MediatR;
using Terminex.Common.Results;
using Terminex.Common.Primitives;
using Shared.Kernel.ValueObjects;
using MoneyFlow.Ledger.Domain.ValueObjects.Accounts;
using MoneyFlow.Ledger.Application.Abstractions.UnitOfWork;
using MoneyFlow.Ledger.Application.Abstractions.Repositories.Accounts;

namespace MoneyFlow.Ledger.Application.Features.Accounts.Commands.Update
{
    public sealed class UpdateAccountHandler(IAccountRepository repository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateAccountCommand, Result<Nothing>>
    {
        public async Task<Result<Nothing>> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            var maybeAccount = await repository.GetByAsync(account => account.UserId == request.UserId && account.Id == request.Id);

            if (maybeAccount.IsNone)
                return Error.NotFound("Счет не найден!");

            maybeAccount.Value.UpdateName(AccountName.Create(request.Name));
            maybeAccount.Value.UpdateTypeAccountId(TypeAccountId.Create(request.TypeAccountId));
            maybeAccount.Value.UpdateCurrencyId(CurrencyId.Create(request.CurrencyId));
            maybeAccount.Value.UpdateBalance(Money.Create(request.Balance));
            maybeAccount.Value.UpdateIsActive(request.IsActive);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Nothing.Value;
        }
    }
}