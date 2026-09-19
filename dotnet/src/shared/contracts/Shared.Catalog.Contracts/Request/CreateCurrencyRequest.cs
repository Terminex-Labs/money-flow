namespace Shared.Catalog.Contracts.Request
{
    public sealed record CreateCurrencyRequest(string ShortName, string Unicode, string FullName);
}