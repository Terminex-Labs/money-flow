using MoneyFlow.Bff.Services;
using Terminex.Security.Cryptography;
using Terminex.Security.Abstraction.Interfaces;

namespace MoneyFlow.Bff.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseServices(this IServiceCollection services)
        {
            services.AddSingleton<IJwtReader, JwtReader>();
            services.AddSingleton<ICryptoService, CryptoService>();

            return services;
        }
    }
}