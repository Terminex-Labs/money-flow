using Shared.Kernel.Primitives;
using Shared.Kernel.ValueObjects;
using MoneyFlow.Catalog.Domain.ValueObjects.Currencies;

namespace MoneyFlow.Catalog.Domain.Models
{
    public sealed class Currency : Aggregate<CurrencyId>
    {
        public ShortName ShortName { get; private set; }
        public CurrencyUnicode Unicode { get; private set; }
        public FullName FullName { get; private set;}

        private Currency() { }
        private Currency(ShortName shortName, CurrencyUnicode unicode, FullName fullName) : base(CurrencyId.New)
        {
            ShortName = shortName;
            Unicode = unicode;
            FullName = fullName;
        }

        public static Currency Create(ShortName shortName, CurrencyUnicode unicode, FullName fullName) 
            => new (shortName, unicode, fullName);

        public void UpdateShortName(ShortName shortName)
            => ShortName = shortName;

        public void UpdateUnicode(CurrencyUnicode unicode)
            => Unicode = unicode;

        public void UpdateFullName(FullName fullName)
            => FullName = fullName;
    }
}