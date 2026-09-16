using MediatR;
using Terminex.Common.Results;
using Terminex.Common.Primitives;

namespace MoneyFlow.Catalog.Application.Features.TypeAccounts.Commands.Update
{
    public sealed record UpdateTypeAccountCommand(Guid Id, string Name) : IRequest<Result<Nothing>>;
}