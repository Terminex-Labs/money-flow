using Microsoft.Extensions.DependencyInjection;
using MoneyFlow.Catalog.Application.Abstractions.Repositories.Currencies;
using MoneyFlow.Catalog.Infrastructure.Persistence.Repositories.Currencies;
using MoneyFlow.Catalog.Application.Abstractions.Repositories.TypeAccounts;
using MoneyFlow.Catalog.Infrastructure.Persistence.Repositories.TypeAccounts;

namespace MoneyFlow.Catalog.Infrastructure.Extensions
{
    public static class RepositoryServiceCollectionExtension
    {
        public static IServiceCollection UseRepository(this IServiceCollection services)
        {
            services.AddScoped<ICurrencyReadOnlyRepository, CurrencyReadOnlyRepository>();
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            
            services.AddScoped<ITypeAccountReadOnlyRepository, TypeAccountReadOnlyRepository>();
            services.AddScoped<ITypeAccountRepository, TypeAccountRepository>();

            return services;
        }
    }
}