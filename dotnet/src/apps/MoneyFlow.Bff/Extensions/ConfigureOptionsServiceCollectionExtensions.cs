using System.Text.Json;

namespace MoneyFlow.Bff.Extensions
{
    public static class ConfigureOptionsServiceCollectionExtensions
    {
        public static IServiceCollection UseConfigureOptions(this IServiceCollection services)
        {
            services.Configure<JsonSerializerOptions>(options => options.PropertyNameCaseInsensitive = true);

            return services;
        }
    }
}