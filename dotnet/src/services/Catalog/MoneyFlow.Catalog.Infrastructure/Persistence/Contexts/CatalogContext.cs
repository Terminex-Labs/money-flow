using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MoneyFlow.Catalog.Domain.Models;

namespace MoneyFlow.Catalog.Infrastructure.Persistence.Contexts
{
    public sealed class CatalogContext(DbContextOptions<CatalogContext> options) : DbContext(options)
    {
        public DbSet<Currency> Currencies { get; set;} = null!;
        public DbSet<TypeAccount> TypeAccounts { get; set;} = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}