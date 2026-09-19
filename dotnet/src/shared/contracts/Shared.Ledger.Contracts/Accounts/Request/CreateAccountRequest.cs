namespace Shared.Ledger.Contracts.Accounts.Request
{
    public sealed record CreateAccountRequest(string UserId, string Name, string TypeAccountId, string CurrencyId, decimal Balance, bool IsActive);
}