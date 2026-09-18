using Npgsql;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoneyFlow.Ledger.Infrastructure.Persistence;
using MoneyFlow.Ledger.Application.Abstractions.UnitOfWork;
using MoneyFlow.Ledger.Infrastructure.Persistence.Contexts;

namespace MoneyFlow.Ledger.Infrastructure.Extensions
{
    public static class DataBaseServiceCollectionExtension
    {
        public static IServiceCollection UsePostgres(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LedgerContext>(options => options.UseNpgsql(configuration["DataBase:ConnectionStrings:Postgres"]));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection UseDapper(this IServiceCollection services, IConfiguration configuration)
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            services.AddTransient<IDbConnection>(serviceProvider => new NpgsqlConnection(configuration["DataBase:ConnectionStrings:Postgres"]));

            return services;
        }
    }
}