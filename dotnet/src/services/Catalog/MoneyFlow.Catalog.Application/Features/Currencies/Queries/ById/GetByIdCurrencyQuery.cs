using MediatR;
using Terminex.Common.Results;
using Shared.Catalog.Contracts.Response;

namespace MoneyFlow.Catalog.Application.Features.Currencies.Queries.ById
{
    public sealed record GetByIdCurrencyQuery(Guid Id) : IRequest<Result<CurrencyResponse>>;
}