using MediatR;
using Terminex.Common.Results;
using Terminex.Common.Primitives;

namespace MoneyFlow.Ledger.Application.Features.Accounts.Commands.Unfreeze
{
    public sealed record UnfreezeAccountCommand(Guid UserId, Guid Id) : IRequest<Result<Nothing>>;
}