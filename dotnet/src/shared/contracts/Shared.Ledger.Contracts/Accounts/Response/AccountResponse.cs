namespace Shared.Ledger.Contracts.Accounts.Response
{
    public sealed record AccountResponse(Guid Id, string Name, Guid TypeAccountId, Guid CurrencyId, decimal Balance, bool IsActive, Guid UserId);
}