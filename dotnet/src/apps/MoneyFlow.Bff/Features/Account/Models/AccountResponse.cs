namespace MoneyFlow.Bff.Features.Account.Models
{
    public sealed record AccountResponse(string Id, string Name, AccountDataCurrencyResponse? Currency, string? TypeAccountName, decimal Balance, bool IsActive);

    public sealed record AccountDataCurrencyResponse(string Id, string ShortName, string Unicode, string FullName);
}