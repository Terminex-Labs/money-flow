using Shared.Kernel.Errors;
using Terminex.Common.Guard;
using Terminex.Common.Results;
using Shared.Kernel.Exceptions;
using System.Text.RegularExpressions;

namespace MoneyFlow.Catalog.Domain.ValueObjects.TypesAccounts
{
    public readonly record struct TypeTransactionName
    {
        public string Value { get; }

        private readonly static string pattern = @"^[a-zA-Zа-яА-ЯёЁ ]+$";

        private TypeTransactionName(string value) => Value = value;

        public static TypeTransactionName Create(string value)
        {
            Guard.Against.That(string.IsNullOrWhiteSpace(value), () => new StringEmptyException(Error.Empty($"Был передан пустой string в {nameof(TypeTransactionName)}!")));
            Guard.Against.That(!Regex.IsMatch(value, pattern), () => new IncorrectNameException(Error.New(AppErrors.IncorrectName, "Используйте только буквы русского или латинского алфавита!")));

            return new TypeTransactionName(value);
        }

        public override string ToString() => Value;
        public static implicit operator string(TypeTransactionName value) => value.Value;
    }
}