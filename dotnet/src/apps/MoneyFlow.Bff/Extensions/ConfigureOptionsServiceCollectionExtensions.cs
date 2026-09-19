using System.Text.Json;
using Shared.Api.Extensions;

namespace MoneyFlow.Bff.Extensions
{
    public static class ConfigureOptionsServiceCollectionExtensions
    {
        public static IServiceCollection UseConfigureOptions(this IServiceCollection services)
        {
            services.Configure<JsonSerializerOptions>(options => options.AddTerminexDefaults());

            return services;
        }
    }
}