using MediatR;
using Terminex.Common.Results;
using Shared.Catalog.Contracts.Response;
using MoneyFlow.Catalog.Application.Abstractions.Repositories.Currencies;

namespace MoneyFlow.Catalog.Application.Features.Currencies.Queries.ById
{
    public sealed class GetByIdCurrencyHandler(ICurrencyReadOnlyRepository repository) : IRequestHandler<GetByIdCurrencyQuery, Result<CurrencyResponse>>
    {
        public async Task<Result<CurrencyResponse>> Handle(GetByIdCurrencyQuery request, CancellationToken cancellationToken)
        {
            var currency = await repository.GetByIdAsync(request.Id, cancellationToken);

            if (currency == null)
                return Error.NotFound("Валюта не найдена!");
            
            return currency;
        }
    }
}