using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MoneyFlow.Ledger.Domain.Models;

namespace MoneyFlow.Ledger.Infrastructure.Persistence.Contexts
{
    public sealed class LedgerContext(DbContextOptions<LedgerContext> options) : DbContext(options)
    {
        public DbSet<Account> Accounts { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}