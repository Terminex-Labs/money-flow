using MediatR;
using Terminex.Common.Results;
using Shared.Catalog.Contracts.Response;

namespace MoneyFlow.Catalog.Application.Features.TypeAccounts.Commands.Create
{
    public sealed record CreateTypeAccountCommand(string Name) : IRequest<Result<CreatedTypeAccountResponse>>;
}