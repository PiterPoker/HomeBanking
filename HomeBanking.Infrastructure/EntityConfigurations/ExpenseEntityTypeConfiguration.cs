using HomeBanking.Domain.AggregatesModel.CategoryAggregate;
using HomeBanking.Domain.AggregatesModel.WalletAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeBanking.Infrastructure.EntityConfigurations
{
    internal class ExpenseEntityTypeConfiguration
        : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.ToTable("expenses", HomeBankingContext.DEFAULT_SCHEMA);

            builder.HasKey(o => o.Id);

            builder.Ignore(b => b.DomainEvents);

            builder.Property(u => u.Id)
                .UseHiLo("expenses_Id_seq", HomeBankingContext.DEFAULT_SCHEMA);

            //Cost
            builder
                .Property<decimal>("_cost")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Cost")
                .IsRequired();

            //Comment
            builder
                .Property<string>("_comment")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Comment")
                .IsRequired(false);

            //Create
            builder
                .Property<DateTime>("_create")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Create")
                .IsRequired();

            //CategoryId
            builder
                .Property<int>("_categoryId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("CategoryId")
                .IsRequired();

            //FamilyId
            builder
                .Property<int>("_familyId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("FamilyId")
                .IsRequired();
        }
    }
}
