using System.Text.Json;
using System.Text.Json.Serialization;

namespace Shared.Api.Extensions
{
    public static class JsonOptionsExtensions
    {
        public static void AddTerminexDefaults(this JsonSerializerOptions options)
        {
            options.NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString;
            options.PropertyNameCaseInsensitive = true;
        }
    }
}