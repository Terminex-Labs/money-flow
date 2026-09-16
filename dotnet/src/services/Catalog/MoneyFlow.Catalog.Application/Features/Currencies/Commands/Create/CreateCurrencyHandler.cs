using MediatR;
using Terminex.Common.Results;
using MoneyFlow.Catalog.Domain.Models;
using Shared.Catalog.Contracts.Response;
using MoneyFlow.Catalog.Domain.ValueObjects.Currencies;
using MoneyFlow.Catalog.Application.Abstractions.UnitOfWork;
using MoneyFlow.Catalog.Application.Abstractions.Repositories.Currencies;

namespace MoneyFlow.Catalog.Application.Features.Currencies.Commands.Create
{
    public sealed class CreateCurrencyHandler(ICurrencyRepository repository, IUnitOfWork unitOfWork) : IRequestHandler<CreateCurrencyCommand, Result<CreatedCurrencyResponse>>
    {
        public async Task<Result<CreatedCurrencyResponse>> Handle(CreateCurrencyCommand request, CancellationToken cancellationToken)
        {
            var currency = Currency.Create
            (
                ShortName.Create(request.ShortName), 
                CurrencyUnicode.Create(request.Unicode),
                FullName.Create(request.FullName)
            );

            await repository.AddAsync(currency, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return new CreatedCurrencyResponse(currency.Id);
        }
    }
}