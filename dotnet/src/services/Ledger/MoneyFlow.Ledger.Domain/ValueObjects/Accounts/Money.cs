using Terminex.Common.Guard;

namespace MoneyFlow.Ledger.Domain.ValueObjects.Accounts
{
    public readonly record struct Money
    {
        public decimal Value { get; }

        private Money(decimal value) => Value = value;

        public static Money Zero => new (decimal.Zero);

        public static Money Create(decimal value) => new (value);

        #region Операторы

        public static Money operator +(Money a, Money b) => new Money(a.Value + b.Value);

        public static Money operator -(Money a, Money b) => new Money(a.Value - b.Value);

        public static Money operator *(Money money, decimal multiplier) => new(money.Value * multiplier);

        public static Money operator /(Money money, decimal divisor)
        {
            Guard.Against.That(divisor == 0, () => new DivideByZeroException("Делитель не может быть равен нулю"));
            return new Money(money.Value / divisor);
        }

        public static bool operator >(Money a, Money b) => a.Value > b.Value;

        public static bool operator <(Money a, Money b) => a.Value < b.Value;
        
        public static bool operator >=(Money a, Money b) => a.Value >= b.Value;

        public static bool operator <=(Money a, Money b) => a.Value <= b.Value;

        #endregion

        public override string ToString() => Value.ToString();
        public static implicit operator decimal(Money money) => money.Value;
    }
}