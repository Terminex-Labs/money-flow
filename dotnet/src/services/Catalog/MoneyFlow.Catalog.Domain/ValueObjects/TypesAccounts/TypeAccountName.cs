using Shared.Kernel.Errors;
using Terminex.Common.Guard;
using Terminex.Common.Results;
using Shared.Kernel.Exceptions;
using System.Text.RegularExpressions;

namespace MoneyFlow.Catalog.Domain.ValueObjects.TypesAccounts
{
    public readonly record struct TypeAccountName
    {
        public string Value { get; }

        private readonly static string pattern = @"^[a-zA-Zа-яА-ЯёЁ ]+$";

        private TypeAccountName(string value) => Value = value;

        public static TypeAccountName Create(string value)
        {
            Guard.Against.That(string.IsNullOrWhiteSpace(value), () => new StringEmptyException(Error.Empty($"Был передан пустой string в {nameof(TypeAccountName)}!")));
            Guard.Against.That(!Regex.IsMatch(value, pattern), () => new IncorrectNameException(Error.New(AppErrors.IncorrectName, "Используйте только буквы русского или латинского алфавита!")));

            return new TypeAccountName(value);
        }

        public override string ToString() => Value;
        public static implicit operator string(TypeAccountName value) => value.Value;
    }
}