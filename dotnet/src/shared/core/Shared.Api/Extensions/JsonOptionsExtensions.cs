using System.Text.Json;

namespace Shared.Api.Extensions
{
    public static class JsonOptionsExtensions
    {
        public static void AddTerminexDefaults(this JsonSerializerOptions options)
        {
            options.PropertyNameCaseInsensitive = true;
        }
    }
}