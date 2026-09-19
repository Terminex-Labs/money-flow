using MediatR;
using Terminex.Common.Results;
using Terminex.Common.Primitives;

namespace MoneyFlow.Catalog.Application.Features.Currencies.Commands.Update
{
    public sealed record UpdateCurrencyCommand(Guid Id, string ShortName, string Unicode, string FullName) : IRequest<Result<Nothing>>;
}