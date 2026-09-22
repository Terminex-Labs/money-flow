namespace MoneyFlow.Bff.Features.Account.Models
{
    public sealed record AccountResponse(string Id, string Name, AccountDataTypeAccountResponse? TypeAccount, AccountDataCurrencyResponse? Currency, decimal Balance, bool IsActive);

    public sealed record AccountDataTypeAccountResponse(string Id, string Name);

    public sealed record AccountDataCurrencyResponse(string Id, string ShortName, string Unicode, string FullName);
}