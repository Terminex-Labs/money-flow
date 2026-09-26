using MediatR;
using Terminex.Common.Results;
using Shared.Catalog.Contracts.Response;

namespace MoneyFlow.Catalog.Application.Features.TypeTransactions.Queries.All
{
    public sealed record GetAllTypeTransactionQuery() : IRequest<Result<List<TypeTransactionResponse>>>;
}