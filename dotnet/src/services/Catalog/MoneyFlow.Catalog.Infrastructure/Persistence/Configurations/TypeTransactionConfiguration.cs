using Shared.Kernel.ValueObjects;
using Microsoft.EntityFrameworkCore;
using MoneyFlow.Catalog.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyFlow.Catalog.Domain.ValueObjects.TypesAccounts;
using MoneyFlow.Catalog.Infrastructure.Persistence.Constants;

namespace MoneyFlow.Catalog.Infrastructure.Persistence.Configurations
{
    internal sealed class TypeTransactionConfiguration : IEntityTypeConfiguration<TypeTransaction>
    {
        public void Configure(EntityTypeBuilder<TypeTransaction> builder)
        {
            builder.ToTable("type_transactions");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedNever()
                .HasConversion(inDB => inDB.Value, outDB => TypeTransactionId.Create(outDB));

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .UseCollation(PostgresConstants.COLLATION_NAME)
                .HasConversion(inDB => inDB.Value, outDB => TypeTransactionName.Create(outDB));
        }
    }
}