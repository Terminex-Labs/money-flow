using MediatR;
using Terminex.Common.Results;
using Terminex.Common.Primitives;

namespace MoneyFlow.Catalog.Application.Features.TypeTransactions.Commands.Delete
{
    public sealed record DeleteTypeTransactionCommand(Guid Id) : IRequest<Result<Nothing>>;
}