using MediatR;
using Terminex.Common.Results;
using Shared.Ledger.Contracts.Accounts.Response;

namespace MoneyFlow.Ledger.Application.Features.Accounts.Queries.ById
{
    public sealed record GetByIdAccountQuery(Guid UserId, Guid Id) : IRequest<Result<AccountResponse>>;
}