using MediatR;
using Terminex.Common.Results;
using Terminex.Common.Primitives;

namespace MoneyFlow.Catalog.Application.Features.TypeAccounts.Commands.Delete
{
    public sealed record DeleteTypeAccountCommand(Guid Id) : IRequest<Result<Nothing>>;
}