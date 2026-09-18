using Npgsql;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoneyFlow.Catalog.Infrastructure.Persistence;
using MoneyFlow.Catalog.Application.Abstractions.UnitOfWork;
using MoneyFlow.Catalog.Infrastructure.Persistence.Contexts;

namespace MoneyFlow.Catalog.Infrastructure.Extensions
{
    public static class DataBaseServiceCollectionExtension
    {
        public static IServiceCollection UsePostgres(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CatalogContext>(options => options.UseNpgsql(configuration["DataBase:ConnectionStrings:Postgres"]));
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