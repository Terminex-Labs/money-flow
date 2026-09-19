using MoneyFlow.Bff.Handlers;
using MoneyFlow.Files.Client;
using MoneyFlow.Ledger.Client;
using MoneyFlow.Catalog.Client;
using Shared.Kernel.Extensions;
using Shared.Client.Abstraction;
using MoneyFlow.IdentityHub.Client;
using MoneyFlow.Authentication.Client;

namespace MoneyFlow.Bff.Extensions
{
    public static class HttpClientsServiceCollectionExtensions
    {
        public static IServiceCollection UseHttpClient(this IServiceCollection services, IConfiguration configuration)
        {
            string authUri = configuration["ServicesUrl:AuthService"].ThrowOrReturn("Строка подключения `ServicesUrl:AuthService` была пуста!");
            string identityHubUri = configuration["ServicesUrl:IdentityHub"].ThrowOrReturn("Строка подключения `ServicesUrl:IdentityHub` была пуста!");
            string minervaUri = configuration["ServicesUrl:Minerva"].ThrowOrReturn("Строка подключения `ServicesUrl:Minerva` была пуста!");
            string catalogUri = configuration["ServicesUrl:Catalog"].ThrowOrReturn("Строка подключения `ServicesUrl:Catalog` была пуста!");
            string ledgerUri = configuration["ServicesUrl:Ledger"].ThrowOrReturn("Строка подключения `ServicesUrl:Ledger` была пуста!");

            services.AddHttpContextAccessor();
            services.AddTransient<AccessTokenHandler>();

            services.AddHttpClient<IAuthClient, AuthClient>(client => client.BaseAddress = new Uri(authUri)).AddHttpMessageHandler<AccessTokenHandler>();

            services.AddHttpClient<IIdentityHubClient, IdentityHubClient>(client => client.BaseAddress = new Uri(identityHubUri)).AddHttpMessageHandler<AccessTokenHandler>();

            services.AddHttpClient<IFileClient, FileClient>(client => client.BaseAddress = new Uri(minervaUri)).AddHttpMessageHandler<AccessTokenHandler>();

            string catalog = "CatalogService";
            services.AddHttpClient(catalog, client => client.BaseAddress = new Uri(catalogUri)).AddHttpMessageHandler<AccessTokenHandler>();
            services.AddHttpClient<ICurrencyClient, CurrencyClient>(catalog);
            services.AddHttpClient<ITypeAccountClient, TypeAccountClient>(catalog);

            services.AddHttpClient<IAccountClient, AccountClient>(client => client.BaseAddress = new Uri(ledgerUri)).AddHttpMessageHandler<AccessTokenHandler>();

            return services;
        }
    }
}