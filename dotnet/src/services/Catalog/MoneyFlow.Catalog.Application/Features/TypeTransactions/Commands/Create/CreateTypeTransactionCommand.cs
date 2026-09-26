using MediatR;
using Terminex.Common.Results;
using Shared.Catalog.Contracts.Response;

namespace MoneyFlow.Catalog.Application.Features.TypeTransactions.Commands.Create
{
    public sealed record CreateTypeTransactionCommand(string Name) : IRequest<Result<CreatedTypeTransactionResponse>>;
}