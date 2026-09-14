using Shared.Kernel.Errors;
using Terminex.Common.Guard;
using Terminex.Common.Results;
using Shared.Kernel.Exceptions;
using System.Text.RegularExpressions;

namespace MoneyFlow.Catalog.Domain.ValueObjects.Currencies
{
    public readonly record struct FullName
    {
        public string Value { get; }
        
        public const int MAX_LENGTH = 3;
        private readonly static string pattern = @"^[A-Z]+$";

        private FullName(string value) => Value = value;

        public static FullName Create(string value)
        {
            Guard.Against.That(string.IsNullOrWhiteSpace(value), () => new StringEmptyException(Error.Empty($"Был передан пустой string в {nameof(FullName)}!")));
            Guard.Against.That(!Regex.IsMatch(value, pattern), () => new IncorrectNameException(Error.New(AppErrors.IncorrectName, "Используйте только буквы латинского алфавита!")));

            return new FullName(value);
        }

        public override string ToString() => Value;
        public static implicit operator string(FullName value) => value.Value;
    }
}