using Terminex.Common.Guard;
using Terminex.Common.Results;
using Shared.Kernel.Exceptions;

namespace Shared.Kernel.ValueObjects
{
    public readonly record struct CurrencyId
    {
        public Guid Value { get; }

        private CurrencyId(Guid value) => Value = value;

        public static CurrencyId New => new (Guid.CreateVersion7());

        public static CurrencyId Create(Guid value)
        {
            Guard.Against.That(value == Guid.Empty, () => new InvalidIdentifierException(Error.Empty($"Был передан пустой Guid в {nameof(CurrencyId)}!")));

            return new CurrencyId(value);
        }

        public override string ToString() => Value.ToString();
        public static implicit operator Guid(CurrencyId value) => value.Value;
        public static implicit operator string(CurrencyId value) => value.ToString();
    }
}