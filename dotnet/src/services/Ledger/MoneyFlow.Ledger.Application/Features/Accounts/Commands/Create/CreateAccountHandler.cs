using MediatR;
using Terminex.Common.Results;
using Shared.Kernel.ValueObjects;
using MoneyFlow.Ledger.Domain.Models;
using Shared.Ledger.Contracts.Accounts.Response;
using MoneyFlow.Ledger.Domain.ValueObjects.Accounts;
using MoneyFlow.Ledger.Application.Abstractions.UnitOfWork;
using MoneyFlow.Ledger.Application.Abstractions.Repositories.Accounts;

namespace MoneyFlow.Ledger.Application.Features.Accounts.Commands.Create
{
    public sealed class CreateAccountHandler(IAccountRepository repository, IUnitOfWork unitOfWork) : IRequestHandler<CreateAccountCommand, Result<CreatedAccountResponse>>
    {
        public async Task<Result<CreatedAccountResponse>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = Account.Create
            (
                AccountName.Create(request.Name),
                TypeAccountId.Create(request.TypeAccountId),
                CurrencyId.Create(request.CurrencyId),
                Money.Create(request.Balance),
                request.IsActive
            );

            await repository.AddAsync(account, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return new CreatedAccountResponse(account.Id);
        }
    }
}