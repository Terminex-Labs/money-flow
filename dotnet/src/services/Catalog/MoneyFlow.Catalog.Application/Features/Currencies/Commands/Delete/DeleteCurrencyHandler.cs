using MediatR;
using Terminex.Common.Results;
using Terminex.Common.Primitives;
using MoneyFlow.Catalog.Application.Abstractions.UnitOfWork;
using MoneyFlow.Catalog.Application.Abstractions.Repositories.Currencies;

namespace MoneyFlow.Catalog.Application.Features.Currencies.Commands.Delete
{
    public sealed class DeleteCurrencyHandler(ICurrencyRepository repository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteCurrencyCommand, Result<Nothing>>
    {
        public async Task<Result<Nothing>> Handle(DeleteCurrencyCommand request, CancellationToken cancellationToken)
        {
            var maybeCurrency = await repository.GetByAsync(currency => currency.Id == request.Id);

            if (maybeCurrency.IsNone)
                return Error.NotFound("Валюта не найдена!");

            repository.Remove(maybeCurrency.Value);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Nothing.Value;
        }
    }
}