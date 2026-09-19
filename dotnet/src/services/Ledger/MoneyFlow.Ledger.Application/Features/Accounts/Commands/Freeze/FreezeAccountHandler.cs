using MediatR;
using Terminex.Common.Results;
using Terminex.Common.Primitives;
using MoneyFlow.Ledger.Application.Abstractions.UnitOfWork;
using MoneyFlow.Ledger.Application.Abstractions.Repositories.Accounts;

namespace MoneyFlow.Ledger.Application.Features.Accounts.Commands.Freeze
{
    public sealed class FreezeAccountHandler(IAccountRepository repository, IUnitOfWork unitOfWork) : IRequestHandler<FreezeAccountCommand, Result<Nothing>>
    {
        public async Task<Result<Nothing>> Handle(FreezeAccountCommand request, CancellationToken cancellationToken)
        {
            var maybeAccount = await repository.GetByAsync(account => account.UserId == request.UserId && account.Id == request.Id);

            if (maybeAccount.IsNone)
                return Error.NotFound("Счет не найден!");
            
            maybeAccount.Value.Freeze();
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Nothing.Value;
        }
    }
}