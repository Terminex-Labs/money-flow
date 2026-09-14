using Terminex.Common.Guard;
using Terminex.Common.Results;
using Shared.Kernel.Exceptions;

namespace MoneyFlow.Ledger.Domain.ValueObjects.Accounts
{
    public readonly record struct AccountId
    {
        public Guid Value { get; }

        private AccountId(Guid value) => Value = value;

        public static AccountId New => new (Guid.CreateVersion7());

        public static AccountId Create(Guid value)
        {
            Guard.Against.That(value == Guid.Empty, () => new InvalidIdentifierException(Error.Empty($"Был передан пустой Guid в {nameof(AccountId)}!")));

            return new AccountId(value);
        }

        public override string ToString() => Value.ToString();
        public static implicit operator Guid(AccountId value) => value.Value;
    }
}