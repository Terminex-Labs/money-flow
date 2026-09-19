using Microsoft.Extensions.DependencyInjection;
using MoneyFlow.Ledger.Application.Abstractions.Repositories.Accounts;
using MoneyFlow.Ledger.Infrastructure.Persistence.Repositories.Accounts;

namespace MoneyFlow.Ledger.Infrastructure.Extensions
{
    public static class RepositoryServiceCollectionExtension
    {
        public static IServiceCollection UseRepository(this IServiceCollection services)
        {
            services.AddScoped<IAccountReadOnlyRepository, AccountReadOnlyRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            return services;
        }
    }
}