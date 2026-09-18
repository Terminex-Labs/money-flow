namespace Shared.Catalog.Contracts.Response
{
    public sealed record CurrencyResponse(Guid Id, string ShortName, string Unicode, string FullName);
}