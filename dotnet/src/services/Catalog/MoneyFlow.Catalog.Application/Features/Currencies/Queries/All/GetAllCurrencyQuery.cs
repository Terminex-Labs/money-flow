using MediatR;
using Terminex.Common.Results;
using Shared.Catalog.Contracts.Response;

namespace MoneyFlow.Catalog.Application.Features.Currencies.Queries.All
{
    public sealed record GetAllCurrencyQuery : IRequest<Result<List<CurrencyResponse>>>;
}