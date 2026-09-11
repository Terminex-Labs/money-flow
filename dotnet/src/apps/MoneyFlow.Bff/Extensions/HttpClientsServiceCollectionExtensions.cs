using MoneyFlow.Files.Client;
using MoneyFlow.IdentityHub.Client;
using MoneyFlow.Authentication.Client;

namespace MoneyFlow.Bff.Extensions
{
    public static class HttpClientsServiceCollectionExtensions
    {
        public static IServiceCollection UseHttpClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IAuthClient, AuthClient>(client => client.BaseAddress = new Uri(configuration["ServicesUrl:AuthService"]!));

            services.AddHttpClient<IIdentityHubClient, IdentityHubClient>(client => client.BaseAddress = new Uri(configuration["ServicesUrl:IdentityHub"]!));

            services.AddHttpClient<IFileClient, FileClient>(client => client.BaseAddress = new Uri(configuration["ServicesUrl:Minerva"]!));

            return services;
        }
    }
}