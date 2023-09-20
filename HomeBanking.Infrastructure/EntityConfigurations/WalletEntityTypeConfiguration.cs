using HomeBanking.Domain.AggregatesModel.UserAggregate;
using HomeBanking.Domain.AggregatesModel.WalletAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeBanking.Infrastructure.EntityConfigurations
{
    internal class WalletEntityTypeConfiguration
        : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.ToTable("wallets", HomeBankingContext.DEFAULT_SCHEMA);

            builder.HasKey(u => u.Id);

            builder.Ignore(u => u.DomainEvents);

            builder.Property(u => u.Id)
                .UseHiLo("wallets_Id_seq", HomeBankingContext.DEFAULT_SCHEMA);

            //Amount
            builder
                .Property<decimal>("_amount")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Amount")
                .HasDefaultValue(0)
                .IsRequired();

            //OwnerId
            builder
                .Property<int>("_ownerId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("OwnerId")
                .IsRequired();

            //Currency
            builder
                .Property<int>("_currencyId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("CurrencyId")
                .IsRequired();


            builder
               .HasMany(b => b.Expenses)
               .WithOne()
               .HasForeignKey("WalletId")
               .OnDelete(DeleteBehavior.Cascade);

            var navigation = builder.Metadata.FindNavigation(nameof(Wallet.Expenses));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
