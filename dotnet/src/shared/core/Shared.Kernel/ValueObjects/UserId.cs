using Terminex.Common.Guard;
using Terminex.Common.Results;
using Shared.Kernel.Exceptions;

namespace Shared.Kernel.ValueObjects
{
    public readonly record struct UserId
    {
        public Guid Value { get; }

        private UserId(Guid value) => Value = value;

        public static UserId New => new (Guid.CreateVersion7());

        public static UserId Create(Guid value)
        {
            Guard.Against.That(value == Guid.Empty, () => new InvalidIdentifierException(Error.Empty($"Был передан пустой Guid в {nameof(UserId)}!")));

            return new UserId(value);
        }

        public override string ToString() => Value.ToString();
        public static implicit operator Guid(UserId value) => value.Value;
        public static implicit operator string(UserId value) => value.ToString();
    }
}