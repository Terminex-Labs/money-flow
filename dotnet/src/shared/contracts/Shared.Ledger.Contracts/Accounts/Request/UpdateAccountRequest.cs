namespace Shared.Ledger.Contracts.Accounts.Request
{
    public sealed record UpdateAccountRequest(string Id, string Name, string TypeAccountId, string CurrencyId, decimal Balance, bool IsActive);
}