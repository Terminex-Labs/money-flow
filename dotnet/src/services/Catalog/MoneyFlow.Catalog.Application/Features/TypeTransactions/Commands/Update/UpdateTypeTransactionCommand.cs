using MediatR;
using Terminex.Common.Results;
using Terminex.Common.Primitives;

namespace MoneyFlow.Catalog.Application.Features.TypeTransactions.Commands.Update
{
    public sealed record UpdateTypeTransactionCommand(Guid Id, string Name) : IRequest<Result<Nothing>>;
}