namespace MoneyFlow.Bff.Features.Account.Models
{
    public sealed record CreateAccountRequest(string Name, string TypeAccountId, string CurrencyId, decimal Balance, bool IsActive);
}