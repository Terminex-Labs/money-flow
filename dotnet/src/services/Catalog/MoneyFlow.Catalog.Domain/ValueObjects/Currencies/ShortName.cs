using Shared.Kernel.Errors;
using Terminex.Common.Guard;
using Terminex.Common.Results;
using Shared.Kernel.Exceptions;
using System.Text.RegularExpressions;

namespace MoneyFlow.Catalog.Domain.ValueObjects.Currencies
{
    public readonly record struct ShortName
    {
        public string Value { get; }
        
        public const int MAX_LENGTH = 3;
        private readonly static string pattern = @"^[A-Z]+$";

        private ShortName(string value) => Value = value;

        public static ShortName Create(string value)
        {
            Guard.Against.That(string.IsNullOrWhiteSpace(value), () => new StringEmptyException(Error.Empty($"Был передан пустой string в {nameof(ShortName)}!")));
            Guard.Against.That(!Regex.IsMatch(value, pattern), () => new IncorrectNameException(Error.New(AppErrors.IncorrectName, "Используйте только буквы латинского алфавита!")));

            return new ShortName(value);
        }

        public override string ToString() => Value;
        public static implicit operator string(ShortName value) => value.Value;
    }
}