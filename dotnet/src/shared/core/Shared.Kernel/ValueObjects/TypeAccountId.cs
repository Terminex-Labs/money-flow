using Terminex.Common.Guard;
using Terminex.Common.Results;
using Shared.Kernel.Exceptions;

namespace Shared.Kernel.ValueObjects
{
    public readonly record struct TypeAccountId
    {
        public Guid Value { get; }

        private TypeAccountId(Guid value) => Value = value;

        public static TypeAccountId New => new (Guid.CreateVersion7());

        public static TypeAccountId Create(Guid value)
        {
            Guard.Against.That(value == Guid.Empty, () => new InvalidIdentifierException(Error.Empty($"Был передан пустой Guid в {nameof(TypeAccountId)}!")));

            return new TypeAccountId(value);
        }

        public override string ToString() => Value.ToString();
        public static implicit operator Guid(TypeAccountId value) => value.Value;
    }
}