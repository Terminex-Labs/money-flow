using Terminex.Common.Guard;
using Terminex.Common.Results;
using Shared.Kernel.Exceptions;

namespace MoneyFlow.Catalog.Domain.ValueObjects.Currencies
{
    public readonly record struct CurrencyUnicode
    {
        public string Value { get; }

        private CurrencyUnicode(string value) => Value = value;

        public static CurrencyUnicode Create(string value)
        {
            Guard.Against.That(string.IsNullOrWhiteSpace(value), () => new StringEmptyException(Error.Empty($"Был передан пустой string в {nameof(CurrencyUnicode)}!")));

            return new CurrencyUnicode(value);
        }

        public override string ToString() => Value;
        public static implicit operator string(CurrencyUnicode value) => value.Value;
    }
}