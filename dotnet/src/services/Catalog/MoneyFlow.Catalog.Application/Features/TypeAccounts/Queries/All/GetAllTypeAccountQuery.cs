using MediatR;
using Terminex.Common.Results;
using Shared.Catalog.Contracts.Response;

namespace MoneyFlow.Catalog.Application.Features.TypeAccounts.Queries.All
{
    public sealed record GetAllTypeAccountQuery : IRequest<Result<List<TypeAccountResponse>>>;
}