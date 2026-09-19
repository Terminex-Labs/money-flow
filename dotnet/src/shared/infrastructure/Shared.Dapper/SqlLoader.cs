namespace Shared.Dapper
{
    public static class SqlLoader
    {
        public static string Load<TAssembly>(string fileName) where TAssembly : class
        {
            var assembly = typeof(TAssembly).Assembly;
            var resourceName = $"SQL.{fileName}";

            using var stream = assembly.GetManifestResourceStream(typeof(TAssembly), resourceName)
                ?? throw new InvalidOperationException($"Ресурс '{resourceName}' не найден для типа {typeof(TAssembly).Name}");

            using var reader = new StreamReader(stream);

            return reader.ReadToEnd();
        }
    }
}