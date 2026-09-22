using MediatR;
using Terminex.Common.Results;
using Shared.Ledger.Contracts.Accounts.Response;
using MoneyFlow.Ledger.Application.Abstractions.Repositories.Accounts;

namespace MoneyFlow.Ledger.Application.Features.Accounts.Queries.ById
{
    public sealed class GetByIdAccountHandler(IAccountReadOnlyRepository repository) : IRequestHandler<GetByIdAccountQuery, Result<AccountResponse>>
    {
        public async Task<Result<AccountResponse>> Handle(GetByIdAccountQuery request, CancellationToken cancellationToken)
        {
            var account = await repository.GetByIdAsync(request.UserId, request.Id, cancellationToken);

            if (account == null)
                return Error.NotFound("Счёт не найден!");

            return account;
        }
    }
}