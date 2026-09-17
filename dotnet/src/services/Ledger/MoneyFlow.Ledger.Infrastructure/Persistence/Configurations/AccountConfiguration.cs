using Shared.Kernel.ValueObjects;
using Microsoft.EntityFrameworkCore;
using MoneyFlow.Ledger.Domain.Models;
using MoneyFlow.Ledger.Domain.ValueObjects.Accounts;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyFlow.Ledger.Infrastructure.Persistence.Constants;

namespace MoneyFlow.Ledger.Infrastructure.Persistence.Configurations
{
    internal sealed class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("accounts");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedNever()
                .HasConversion(inDB => inDB.Value, outDB => AccountId.Create(outDB));

            builder.Property(x => x.UserId)
                .HasColumnName("user_id")
                .HasConversion(inDB => inDB.Value, outDB => UserId.Create(outDB));

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .UseCollation(PostgresConstants.COLLATION_NAME)
                .HasConversion(inDB => inDB.Value, outDB => AccountName.Create(outDB));

            builder.Property(x => x.TypeAccountId)
                .HasColumnName("type_account_id")
                .HasConversion(inDB => inDB.Value, outDB => TypeAccountId.Create(outDB));

            builder.Property(x => x.CurrencyId)
                .HasColumnName("currency_id")
                .HasConversion(inDB => inDB.Value, outDB => CurrencyId.Create(outDB));

            builder.Property(x => x.Balance)
                .HasColumnName("balance")
                .HasConversion(inDB => inDB.Value, outDB => Money.Create(outDB));

            builder.Property(x => x.IsActive).HasColumnName("is_active");
        }
    }
}