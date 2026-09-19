using MediatR;
using Terminex.Common.Results;
using Terminex.Common.Primitives;
using MoneyFlow.Catalog.Domain.ValueObjects.Currencies;
using MoneyFlow.Catalog.Application.Abstractions.UnitOfWork;
using MoneyFlow.Catalog.Application.Abstractions.Repositories.Currencies;

namespace MoneyFlow.Catalog.Application.Features.Currencies.Commands.Update
{
    public sealed class UpdateCurrencyHandler(ICurrencyRepository repository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateCurrencyCommand, Result<Nothing>>
    {
        public async Task<Result<Nothing>> Handle(UpdateCurrencyCommand request, CancellationToken cancellationToken)
        {
            var maybeCurrency = await repository.GetByAsync(currency => currency.Id == request.Id);

            if (maybeCurrency.IsNone)
                return Error.NotFound("Валюта не найдена!");

            maybeCurrency.Value.UpdateShortName(ShortName.Create(request.ShortName));
            maybeCurrency.Value.UpdateFullName(FullName.Create(request.FullName));
            maybeCurrency.Value.UpdateUnicode(CurrencyUnicode.Create(request.Unicode));

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Nothing.Value;
        }
    }
}