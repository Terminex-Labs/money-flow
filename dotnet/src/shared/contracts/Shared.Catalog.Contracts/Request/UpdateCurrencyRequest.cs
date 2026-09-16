namespace Shared.Catalog.Contracts.Request
{
    public sealed record UpdateCurrencyRequest(string Id, string ShortName, string Unicode, string FullName);
}