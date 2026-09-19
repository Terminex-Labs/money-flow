using Shared.Kernel.Errors;
using Terminex.Common.Guard;
using Terminex.Common.Results;
using Shared.Kernel.Exceptions;
using System.Text.RegularExpressions;

namespace MoneyFlow.Ledger.Domain.ValueObjects.Accounts
{
    public readonly record struct AccountName
    {
        public string Value { get; }

        private readonly static string pattern = @"^[a-zA-Zа-яА-ЯёЁ ]+$";

        private AccountName(string value) => Value = value;

        public static AccountName Create(string value)
        {
            Guard.Against.That(string.IsNullOrWhiteSpace(value), () => new StringEmptyException(Error.Empty($"Был передан пустой string в {nameof(AccountName)}!")));
            Guard.Against.That(!Regex.IsMatch(value, pattern), () => new IncorrectNameException(Error.New(AppErrors.IncorrectName, "Используйте только буквы русского или латинского алфавита!")));

            return new AccountName(value);
        }

        public override string ToString() => Value;
        public static implicit operator string(AccountName value) => value.Value;
    }
}