namespace Shared.Ledger.Contracts.Accounts.Request
{
    public sealed record CreateAccountRequest(string Name, string TypeAccountId, string CurrencyId, decimal Balance, bool IsActive);
}