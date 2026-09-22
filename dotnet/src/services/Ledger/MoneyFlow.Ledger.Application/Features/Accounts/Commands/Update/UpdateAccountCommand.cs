using MediatR;
using Terminex.Common.Results;
using Terminex.Common.Primitives;

namespace MoneyFlow.Ledger.Application.Features.Accounts.Commands.Update
{
    public sealed record UpdateAccountCommand(Guid UserId, Guid Id, string Name, Guid TypeAccountId, Guid CurrencyId, decimal Balance, bool IsActive) : IRequest<Result<Nothing>>;
}