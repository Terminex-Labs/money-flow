using MediatR;
using Terminex.Common.Results;
using Shared.Catalog.Contracts.Response;
using MoneyFlow.Catalog.Application.Abstractions.Repositories.Currencies;

namespace MoneyFlow.Catalog.Application.Features.Currencies.Queries.All
{
    public sealed class GetAllCurrencyHandler(ICurrencyReadOnlyRepository repository) : IRequestHandler<GetAllCurrencyQuery, Result<List<CurrencyResponse>>>
    {
        public async Task<Result<List<CurrencyResponse>>> Handle(GetAllCurrencyQuery request, CancellationToken cancellationToken)
        {
            var currencies = await repository.GetAllAsync(cancellationToken);
            
            return currencies;
        }
    }
}