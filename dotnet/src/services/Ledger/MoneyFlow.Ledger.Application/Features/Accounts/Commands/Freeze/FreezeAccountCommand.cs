using MediatR;
using Terminex.Common.Results;
using Terminex.Common.Primitives;

namespace MoneyFlow.Ledger.Application.Features.Accounts.Commands.Freeze
{
    public sealed record FreezeAccountCommand(Guid Id) : IRequest<Result<Nothing>>;
}