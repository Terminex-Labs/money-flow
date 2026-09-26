using Terminex.Common.Guard;
using Terminex.Common.Results;
using Shared.Kernel.Exceptions;

namespace Shared.Kernel.ValueObjects
{
    public readonly record struct TypeTransactionId
    {
        public Guid Value { get; }

        private TypeTransactionId(Guid value) => Value = value;

        public static TypeTransactionId New => new (Guid.CreateVersion7());

        public static TypeTransactionId Create(Guid value)
        {
            Guard.Against.That(value == Guid.Empty, () => new InvalidIdentifierException(Error.Empty($"Был передан пустой Guid в {nameof(TypeTransactionId)}!")));

            return new TypeTransactionId(value);
        }

        public override string ToString() => Value.ToString();
        public static implicit operator Guid(TypeTransactionId value) => value.Value;
        public static implicit operator string(TypeTransactionId value) => value.ToString();
    }
}