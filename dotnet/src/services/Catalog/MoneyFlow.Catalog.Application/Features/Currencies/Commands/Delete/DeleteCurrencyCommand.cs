using MediatR;
using Terminex.Common.Results;
using Terminex.Common.Primitives;

namespace MoneyFlow.Catalog.Application.Features.Currencies.Commands.Delete
{
    public sealed record DeleteCurrencyCommand(Guid Id) : IRequest<Result<Nothing>>;
}