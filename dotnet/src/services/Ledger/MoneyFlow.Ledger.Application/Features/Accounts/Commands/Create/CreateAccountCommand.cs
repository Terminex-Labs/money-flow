using MediatR;
using Terminex.Common.Results;
using Shared.Ledger.Contracts.Accounts.Response;

namespace MoneyFlow.Ledger.Application.Features.Accounts.Commands.Create
{
    public sealed record CreateAccountCommand(string Name, Guid TypeAccountId, Guid CurrencyId, decimal Balance, bool IsActive) : IRequest<Result<CreatedAccountResponse>>;
}