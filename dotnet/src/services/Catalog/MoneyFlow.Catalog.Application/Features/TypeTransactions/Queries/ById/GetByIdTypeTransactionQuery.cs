using MediatR;
using Terminex.Common.Results;
using Shared.Catalog.Contracts.Response;

namespace MoneyFlow.Catalog.Application.Features.TypeTransactions.Queries.ById
{
    public sealed record GetByIdTypeTransactionQuery(Guid Id) : IRequest<Result<TypeTransactionResponse>>;
}