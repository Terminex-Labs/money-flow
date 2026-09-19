using MediatR;
using Terminex.Common.Results;
using Shared.Ledger.Contracts.Accounts.Response;

namespace MoneyFlow.Ledger.Application.Features.Accounts.Queries.All
{
    public sealed record GetAllAccountQuery : IRequest<Result<List<AccountResponse>>>;
}