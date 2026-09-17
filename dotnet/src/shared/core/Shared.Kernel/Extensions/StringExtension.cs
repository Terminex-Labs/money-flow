namespace Shared.Kernel.Extensions
{
    public static class StringExtension
    {
        public static string ThrowOrReturn(this string? value, string? errorMessage)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException(errorMessage ?? "Значение было пустым!");

            return value;
        }
    }
}