using MediatR;
using Terminex.Common.Results;
using Shared.Catalog.Contracts.Response;

namespace MoneyFlow.Catalog.Application.Features.TypeAccounts.Queries.ById
{
    public sealed record GetByIdTypeAccountQuery(Guid Id) : IRequest<Result<TypeAccountResponse>>;
}