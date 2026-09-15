using MediatR;
using Terminex.Common.Results;
using Terminex.Common.Primitives;

namespace MoneyFlow.Ledger.Application.Features.Accounts.Commands.UpdateName
{
    public sealed record UpdateAccountNameCommand(Guid Id, string Name) : IRequest<Result<Nothing>>;
}