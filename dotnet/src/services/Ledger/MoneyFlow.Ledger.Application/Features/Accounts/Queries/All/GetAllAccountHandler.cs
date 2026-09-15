using MediatR;
using Terminex.Common.Results;
using Shared.Ledger.Contracts.Accounts.Response;
using MoneyFlow.Ledger.Application.Abstractions.Repositories.Accounts;

namespace MoneyFlow.Ledger.Application.Features.Accounts.Queries.All
{
    public sealed class GetAllAccountHandler(IAccountReadOnlyRepository repository) : IRequestHandler<GetAllAccountQuery, Result<List<AccountResponse>>>
    {
        public async Task<Result<List<AccountResponse>>> Handle(GetAllAccountQuery request, CancellationToken cancellationToken)
        {
            var accounts = await repository.GetAllAsync(cancellationToken);

            return accounts.ToList();
        }
    }
}