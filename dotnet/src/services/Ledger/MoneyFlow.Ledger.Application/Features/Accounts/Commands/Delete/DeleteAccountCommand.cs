using MediatR;
using Terminex.Common.Results;
using Terminex.Common.Primitives;

namespace MoneyFlow.Ledger.Application.Features.Accounts.Commands.Delete
{
    public sealed record DeleteAccountCommand(Guid Id) : IRequest<Result<Nothing>>;
}