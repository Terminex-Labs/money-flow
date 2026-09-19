using MediatR;
using Terminex.Common.Results;
using Shared.Catalog.Contracts.Response;

namespace MoneyFlow.Catalog.Application.Features.Currencies.Commands.Create
{
    public sealed record CreateCurrencyCommand(string ShortName, string Unicode, string FullName) : IRequest<Result<CreatedCurrencyResponse>>;
}