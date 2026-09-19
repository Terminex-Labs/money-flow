using MediatR;
using Terminex.Common.Results;
using Terminex.Common.Primitives;
using MoneyFlow.Ledger.Application.Abstractions.UnitOfWork;
using MoneyFlow.Ledger.Application.Abstractions.Repositories.Accounts;

namespace MoneyFlow.Ledger.Application.Features.Accounts.Commands.Unfreeze
{
    public sealed class UnfreezeAccountHandler(IAccountRepository repository, IUnitOfWork unitOfWork) : IRequestHandler<UnfreezeAccountCommand, Result<Nothing>>
    {
        public async Task<Result<Nothing>> Handle(UnfreezeAccountCommand request, CancellationToken cancellationToken)
        {
            var maybeAccount = await repository.GetByAsync(account => account.Id == request.Id);

            if (maybeAccount.IsNone)
                return Error.NotFound("Счет не найден!");

            maybeAccount.Value.Unfreeze();
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Nothing.Value;
        }
    }
}