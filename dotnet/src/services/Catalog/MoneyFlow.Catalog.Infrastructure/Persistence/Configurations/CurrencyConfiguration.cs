using Shared.Kernel.ValueObjects;
using Microsoft.EntityFrameworkCore;
using MoneyFlow.Catalog.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyFlow.Catalog.Domain.ValueObjects.Currencies;
using MoneyFlow.Catalog.Infrastructure.Persistence.Constants;

namespace MoneyFlow.Catalog.Infrastructure.Persistence.Configurations
{
    internal sealed class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.ToTable("currencies");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedNever()
                .HasConversion(inDB => inDB.Value, outDB => CurrencyId.Create(outDB));

            builder.Property(x => x.ShortName)
                .HasColumnName("short_name")
                .UseCollation(PostgresConstants.COLLATION_NAME)
                .HasConversion(inDB => inDB.Value, outDB => ShortName.Create(outDB));

            builder.Property(x => x.Unicode)
                .HasColumnName("unicode")
                .UseCollation(PostgresConstants.COLLATION_NAME)
                .HasConversion(inDB => inDB.Value, outDB => CurrencyUnicode.Create(outDB));

            builder.Property(x => x.FullName)
                .HasColumnName("full_name")
                .UseCollation(PostgresConstants.COLLATION_NAME)
                .HasConversion(inDB => inDB.Value, outDB => FullName.Create(outDB));
        }
    }
}