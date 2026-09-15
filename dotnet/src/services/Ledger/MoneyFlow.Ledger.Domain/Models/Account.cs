using Shared.Kernel.Errors;
using Terminex.Common.Results;
using Shared.Kernel.Primitives;
using Shared.Kernel.Exceptions;
using Shared.Kernel.ValueObjects;
using MoneyFlow.Ledger.Domain.ValueObjects.Accounts;

namespace MoneyFlow.Ledger.Domain.Models
{
    public sealed class Account : Aggregate<AccountId>
    {
        public AccountName Name { get; private set; }
        public TypeAccountId TypeAccountId { get; private set; }
        public CurrencyId CurrencyId { get; private set; }
        public Money Balance { get; private set; }
        public bool IsActive { get; private set; }

        private Account() { }
        private Account(AccountName name, TypeAccountId typeAccountId, CurrencyId currencyId, Money balance, bool isActive) : base(AccountId.New)
        {
            Name = name;
            TypeAccountId = typeAccountId;
            CurrencyId = currencyId;
            Balance = balance;
            IsActive = isActive;
        }

        public static Account Create(AccountName name, TypeAccountId typeAccountId, CurrencyId currencyId, Money balance, bool isActive)
            => new (name, typeAccountId, currencyId, balance, isActive);

        public void UpdateName(AccountName name)
        {
            if (!IsActive)
                throw new DomainException(Error.New(AppErrors.IncorrectOperation, "Невозможно обновить не активный счет!"));

            Name = name;
        }

        public void Freeze() => IsActive = false;
        public void Unfreeze() => IsActive = true;
    }
}