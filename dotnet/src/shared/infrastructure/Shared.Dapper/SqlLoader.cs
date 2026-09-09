using System.Reflection;
using System.Text;

namespace Shared.Dapper
{
    public static class SqlLoader
    {
        public static string Load(string repositoryFolderName, string filename, string folderNameForQueries, string baseNamespace, Assembly assembly)
        {
            var sb = new StringBuilder();

            sb.Append($"{baseNamespace}.{repositoryFolderName}");

            sb.Append($".{folderNameForQueries}.{filename}.sql");

            var resourceName = sb.ToString();

            using var stream = assembly!.GetManifestResourceStream(resourceName) ??
                 throw new InvalidOperationException($"SQL файл '{resourceName}' не найден.");

            using var reader = new StreamReader(stream, Encoding.UTF8);
            return reader.ReadToEnd().Trim();
        }
    }
}