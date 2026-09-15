namespace Shared.Ledger.Contracts.Accounts.Response
{
    public sealed record AccountResponse(string Id, string Name, string TypeAccountId, string CurrencyId, string Balance, bool IsActive);
}