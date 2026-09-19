using MediatR;
using Terminex.Common.Results;
using Terminex.Common.Primitives;
using MoneyFlow.Ledger.Application.Abstractions.UnitOfWork;
using MoneyFlow.Ledger.Application.Abstractions.Repositories.Accounts;

namespace MoneyFlow.Ledger.Application.Features.Accounts.Commands.Delete
{
    public sealed class DeleteAccountHandler(IAccountRepository repository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteAccountCommand, Result<Nothing>>
    {
        public async Task<Result<Nothing>> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            var maybeAccount = await repository.GetByAsync(account => account.Id == request.Id);

            repository.Remove(maybeAccount.Value);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Nothing.Value;
        }
    }
}